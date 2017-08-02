using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace Sedic.Application.WebApi.Controllers
{
    public abstract class AuthControllerBase<TUser, TUserDto> : Controller
        where TUser : class
    {
        protected readonly AuthTokenOptions _authTokenOptions;
        protected readonly UserManager<TUser> _userManager;
        protected readonly SignInManager<TUser> _signInManager;

        public AuthControllerBase(AuthTokenOptions authTokenOptions, UserManager<TUser> userManager, SignInManager<TUser> signInManager)
        {
            _authTokenOptions = authTokenOptions;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected abstract TUser GetUserByUsername(string username);
        protected abstract string GetUserId(TUser user);

        private async Task<bool> CheckAuth(string username, string password)
        {
            var user = GetUserByUsername(username); // _userManager.Users.FirstOrDefault(x => x.Username == username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }

        // [HttpPost]
        // [Route("setup")]
        // [AllowAnonymous]
        // public async Task<bool> AddUsers()
        // {
        //     var user = new User
        //     {
        //         UserName = "lemol",
        //         FullName = "Leza",
        //         Email = "lemol@lemol.io"
        //     };

        //    var result = await _userManager.CreateAsync(user, "Lemol!123");
        //    return result.Succeeded;
        // }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody]AuthDto auth)
        {
            if (!await CheckAuth(auth.Username, auth.Password))
            {
                return BadRequest("Invalid credentials");
            }

            var user = GetUserByUsername(auth.Username); //_userManager.Users.FirstOrDefault(x => x.UserName == auth.UserName);

            var claims = new[]
            {
                new Claim("Sub", GetUserId(user))
            };

            var now = DateTime.Now;
            var expires = now.AddSeconds(36000);
            var jwt = new JwtSecurityToken(
                audience: _authTokenOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _authTokenOptions.SigningCredentials,
                issuer: _authTokenOptions.Issuer
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires_in = (int)360000
            };

            var json = JsonConvert.SerializeObject(response);

            return new OkObjectResult(json);
        }

        // [HttpGet]
        // [Authorize]
        // [Route("user")]
        // public abstract Task<TUserDto> GetUser();
        // {
        //     var user = await _userManager.GetUserAsync(User);
        //     var userDto = new UserDto
        //     {
        //         Id = user.Id,
        //         Username = user.UserName,
        //         FullName = user.FullName
        //     };

        //     return userDto;
        // }
    }
}
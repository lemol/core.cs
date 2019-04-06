using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.WebApi.Controllers
{
    public abstract class AuthControllerBase<TUser, TUserDto> : Controller
        where TUser : class
    {
        protected readonly IOptions<JwtConfiguration> _jwtOptions;
        protected readonly UserManager<TUser> _userManager;
        protected readonly SignInManager<TUser> _signInManager;

        public AuthControllerBase(IOptions<JwtConfiguration> jwtOptions, UserManager<TUser> userManager, SignInManager<TUser> signInManager)
        {
            _jwtOptions = jwtOptions;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected abstract TUser GetUserByUsername(string username);
        protected abstract string GetUserId(TUser user);

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody]AuthDto auth)
        {
            var isValidated = await CheckAuth(auth.Username, auth.Password);

            if (!isValidated)
            {
                return BadRequest("Invalid credentials");
            }

            var user = GetUserByUsername(auth.Username);
            var json = GetJwt(GetUserId(user));

            return new OkObjectResult(json);
        }

        #region Helpers
        private string GetJwt(string username)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = _jwtOptions.Value.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_jwtOptions.Value.Expires)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                message = "Login com sucesso",
                data = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds,
                }
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private async Task<bool> CheckAuth(string username, string password)
        {
            var user = GetUserByUsername(username); // _userManager.Users.FirstOrDefault(x => x.Username == username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }
        #endregion
    }
}
using Microsoft.AspNetCore.Mvc;
using Domain.Data;
using Domain.Model.Auditoria;
using Application.Dto.Auditoria;
using Microsoft.AspNetCore.Identity;
using Core.Application.WebApi;
using Core.Application.WebApi.Controllers;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Application.WebApi.Main.Model;
using Microsoft.Extensions.Options;
using Application.Services;
using System.Linq.Expressions;
using Domain.Data.Repositories.Auditoria;

namespace Application.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : AuthControllerBase<User, UserDto>
    {
        private readonly SimpleCrudService<Utilizador, UtilizadorDto> _utilizadorService;
        public AuthController(
            IOptions<JwtConfiguration> jwtOptions
            , UserManager<User> userManager
            , SignInManager<User> signInManager
            , IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IUtilizadorRepository utilizadorRepository)
            : base(jwtOptions, userManager, signInManager)
        {
            _utilizadorService = new SimpleCrudService<Utilizador, UtilizadorDto>(
                mapper
                , unitOfWork
                , utilizadorRepository
            );
        }

        [HttpGet]
        [Route("setup")]
        [AllowAnonymous]
        public async Task<bool> AddUsers()
        {
            var users = _utilizadorService.GetAll<UtilizadorDto>()
                .ToList()
                .Select(x => new User
                {
                    UserName = x.Email,
                    Name = x.Nome,
                    Email = x.Email,
                    UtilizadorId = x.Id
                })
                .ToList();

            foreach(var user in users)
            {
                var result = await _userManager.CreateAsync(user, "Lemol!123");
                Console.WriteLine(result);
            }

            return true;
        }

        protected override User GetUserByUsername(string username)
        {
             return _userManager.Users.FirstOrDefault(x => x.UserName == username);
        }

        protected override string GetUserId(User user)
        {
            return user.Id.ToString();
        }

        [HttpGet]
        [Authorize]
        [Route("user")]
        public async Task<UtilizadorDto> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            var utilizadorDto = _utilizadorService.Find<UtilizadorDto>(user.UtilizadorId.Value);

            return utilizadorDto;
        }
    }

}
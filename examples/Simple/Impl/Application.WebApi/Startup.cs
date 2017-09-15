using Core.Application.WebApi.SimpleInjector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.WebApi.Main.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;
using System.Text;
using Core.Application.WebApi;

namespace Application.WebApi
{
    public class Startup : SimpleInjectorWebApiStartup<DefaultContainer>
    {
        private readonly SecurityKey _signingKey;
        
        public Startup(IHostingEnvironment env)
            : base(env)
        {
            _signingKey = TokenUtils.GetKey();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var provider = Configuration["Data:DefaultConnection:Provider"];
            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];

            switch (provider)
            {
                case "Sqlite":
                    services.AddDbContext<DefaultDbContext>(o => o.UseSqlite(connectionString));
                    services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(connectionString));
                    break;
                case "PostgreSql":
                    services.AddDbContext<DefaultDbContext>(o => o.UseNpgsql(connectionString));
                    services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
                    break;
                case "SqlServer":
                    services.AddDbContext<DefaultDbContext>(o => o.UseSqlServer(connectionString));
                    services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
                    break;
                default:
                    throw new Exception("Erro de provider");
            }

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
                
            WrappedContainer.AddScoped(() =>
                services
                    .BuildServiceProvider()
                    .GetService<ApplicationDbContext>()
            );
            WrappedContainer.AddScoped(() =>
                services
                    .BuildServiceProvider()
                    .GetService<UserManager<User>>()
            );
            WrappedContainer.AddScoped(() =>
                services
                    .BuildServiceProvider()
                    .GetService<SignInManager<User>>()
            );

            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseCors(x =>
                x
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );

            app.UseAuthentication();

            base.Configure(app, env, loggerFactory);
        }
    }
}

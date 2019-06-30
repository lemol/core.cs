using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.IoC.SimpleInjector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Core.Application.WebApi.SimpleInjector
{
  public class SimpleInjectorWebApiStartup<TSimpleInjectorContainer>
      where TSimpleInjectorContainer : SimpleInjectorContainer, new()
  {
    #region Fields
    private readonly InnerSimpleInjectorWebApiStartup _inner;
    protected TSimpleInjectorContainer WrappedContainer => _inner.WrappedContainer;
    protected Container Container => _inner.Container;
    protected IConfigurationRoot Configuration => _inner.Configuration;
    #endregion

    public SimpleInjectorWebApiStartup(IHostingEnvironment env)
    {
      _inner = new InnerSimpleInjectorWebApiStartup(env);
    }

    public virtual void ConfigureServices(IServiceCollection services) => _inner.ConfigureServices(services);
    public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) => _inner.Configure(app, env, loggerFactory);

    class InnerSimpleInjectorWebApiStartup : SimpleInjectorStartup<TSimpleInjectorContainer>, IWebApiStartup
    {
      public TSimpleInjectorContainer WrappedContainer => _wrappedContainer;
      public Container Container => _container;
      public IConfigurationRoot Configuration => _configuration;
      public InnerSimpleInjectorWebApiStartup(IHostingEnvironment env)
          : base(BuildConfiguration(env))
      {
      }

      private static IConfigurationRoot BuildConfiguration(IHostingEnvironment env)
      {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        return builder.Build();
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      public override void ConfigureServices(IServiceCollection services)
      {
        services.AddOptions();
        services.Configure<JwtConfiguration>(Configuration.GetSection("Jwt"));

        IntegrateSimpleInjector(services);

        ConfigureJwtAuthService(services);


        WrappedContainer.AddTransient(() =>
            services
                .BuildServiceProvider()
                .GetService<IOptions<JwtConfiguration>>()
        );

        base.ConfigureServices(services);

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      {
        _container.RegisterMvcControllers(app);
        app.UseMvc();
        base.Verify();
      }

      #region Privates
      protected void IntegrateSimpleInjector(IServiceCollection services)
      {
        // _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
        services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

        services.EnableSimpleInjectorCrossWiring(_container);
        services.UseSimpleInjectorAspNetRequestScoping(_container);
      }

      public void ConfigureJwtAuthService(IServiceCollection services)
      {
        var jwtConfiguration = Configuration.GetSection("Jwt");
        var symmetricKeyAsBase64 = jwtConfiguration["Secret"];
        var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
        var signingKey = new SymmetricSecurityKey(keyByteArray);

        var tokenValidationParameters = new TokenValidationParameters
        {
          // The signing key must match!  
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = signingKey,

          // Validate the JWT Issuer (iss) claim  
          ValidateIssuer = true,
          ValidIssuer = jwtConfiguration["Issuer"],

          // Validate the JWT Audience (aud) claim  
          ValidateAudience = true,
          ValidAudience = jwtConfiguration["Audience"],

          // Validate the token expiry  
          ValidateLifetime = true,

          ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
          o.TokenValidationParameters = tokenValidationParameters;
        });
      }
      #endregion
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.WebApi.SimpleInjector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Simple.Domain.Data;
using Simple.Infrastructure.IoC;

namespace Application.WebApi
{
    public class Startup : SimpleInjectorStartup<DefaultContainer>
    {
        #region Constructors
        public Startup(IHostingEnvironment env)
            : base(env)
        {
        }
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureMyServices(IServiceCollection services)
        {
            services.AddDbContext<SimpleDbContext>(o => o.UseSqlite(
                _configuration["Data:DefaultConnection:ConnectionString"]
            ));

            //  services.AddIdentity<User, Role>()
            //     .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
            //     .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Core.Application.WebApi
{
    public interface IWebApiStartup : Core.Infrastructure.IStartup
    {
        void Configure(IApplicationBuilder app, IHostingEnvironment env);
    }
}
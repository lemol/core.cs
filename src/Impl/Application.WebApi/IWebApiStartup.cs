using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Application.WebApi
{
    public interface IWebApiStartup : Core.Infrastructure.IStartup
    {
        void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory);
    }
}
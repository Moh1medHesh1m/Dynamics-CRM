using Autofac;
using Nop.Core.Infrastructure;
using Nop.Web.Configuration;
using Nop.Web.Services;

namespace Nop.Web.Infrastructure
{
    public class CrmStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CrmSettings>(configuration.GetSection("CrmSettings"));

            services.AddSingleton<ICrmIntegrationService, CrmIntegrationService>();
        }

        public void Configure(IApplicationBuilder application)
        {
            // Nothing needed here for now
        }

        public int Order => 100; // Run after core services
    }
}

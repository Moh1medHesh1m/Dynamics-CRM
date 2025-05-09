using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Configuration;
using Nop.Web.Services;

namespace Nop.Tests.Nop.Web.Tests;

/// <summary>
/// Represents startup class of application
/// </summary>
public class Startup : INopStartup
{
    /// <summary>
    /// Add services to the application and configure service provider
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CrmSettings>(configuration.GetSection(nameof(CrmSettings)));

        services.AddSingleton<ICrmIntegrationService, CrmIntegrationService>();

        // Optional: Configure HttpClient for API calls
        services.AddHttpClient("CRMClient", client =>
        {
            client.BaseAddress = new Uri("https://org7d108b59.crm4.dynamics.com/");
        });
    }

    /// <summary>
    /// Configure the DI container 
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public void ConfigureContainer(IServiceCollection services)
    {

    }

    /// <summary>
    /// Configure the application HTTP request pipeline
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseResponseCompression(); // Example

    }
    public int Order => 1000; 

}
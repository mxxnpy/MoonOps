using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace MoonOps.Api.Configuration;

public static class OpenApiConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
   services.AddOpenApi()
               .AddEndpointsApiExplorer();
    }

    public static void Configure(WebApplication app)
    {
        app.MapOpenApi();
    }
}

using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace MoonOps.Api.Configuration;

public static class ScalarConfiguration
{
    public static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference("/docs");
        }
    }
}

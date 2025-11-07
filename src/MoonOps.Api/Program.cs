#region Namespace Imports
using Scalar.AspNetCore;
#endregion

#region Application Configuration

var builder = WebApplication.CreateBuilder(args);

#region Service Registration

/// <summary>
/// Registers OpenAPI services for API documentation generation.
/// </summary>
builder.Services.AddOpenApi();

#endregion

var app = builder.Build();

#region Middleware Pipeline Configuration

/// <summary>
/// Maps the OpenAPI endpoint for serving the API specification.
/// </summary>
app.MapOpenApi();

/// <summary>
/// Configures Scalar UI for interactive API documentation in development mode.
/// Accessible at /scalar/v1 when running in development environment.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

#endregion

#region Endpoint Mapping

/// <summary>
/// Health check endpoint to verify API availability.
/// Returns the current health status and timestamp.
/// </summary>
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithOpenApi();

#endregion

app.Run();

#endregion

#region Program Class

/// <summary>
/// Make the implicit Program class accessible to tests.
/// Required for integration testing with WebApplicationFactory.
/// </summary>
public partial class Program { }

#endregion

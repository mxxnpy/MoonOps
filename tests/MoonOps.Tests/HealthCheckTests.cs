#region Namespace Imports
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
#endregion

namespace MoonOps.Tests;

#region Health Check Tests

/// <summary>
/// Integration tests for the health check endpoint.
/// Verifies that the API is running and responding correctly.
/// </summary>
public class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
{
    #region Fields

    /// <summary>
    /// Web application factory for creating test clients.
    /// </summary>
    private readonly WebApplicationFactory<Program> _factory;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthCheckTests"/> class.
    /// </summary>
    /// <param name="factory">The web application factory for creating test clients</param>
    public HealthCheckTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #endregion

    #region Test Methods

    /// <summary>
    /// Tests that the health check endpoint returns a healthy status.
    /// </summary>
    [Fact]
    public async Task HealthCheck_ReturnsHealthyStatus()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Healthy", content);
        Assert.Contains("timestamp", content);
    }

    #endregion
}

#endregion

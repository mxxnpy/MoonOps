#region Namespace Imports
#endregion

namespace MoonOps.Application.Models;

#region Health Response Model

/// <summary>
/// Model representing the response from the health check endpoint.
/// Contains information about the API's current health status.
/// </summary>
public class HealthResponseModel
{
    #region Properties

    /// <summary>
    /// Gets or sets the current health status of the API.
    /// Typical values: "Healthy", "Degraded", "Unhealthy".
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the health check was performed.
    /// </summary>
    public DateTime Timestamp { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthResponseModel"/> class.
    /// </summary>
    public HealthResponseModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthResponseModel"/> class with specified values.
    /// </summary>
    /// <param name="status">The health status</param>
    /// <param name="timestamp">The timestamp of the health check</param>
    public HealthResponseModel(string status, DateTime timestamp)
    {
        Status = status;
        Timestamp = timestamp;
    }

    #endregion
}

#endregion

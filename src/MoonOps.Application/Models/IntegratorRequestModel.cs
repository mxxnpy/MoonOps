#region Namespace Imports
#endregion

namespace MoonOps.Application.Models;

#region Integrator Request Model

/// <summary>
/// Model representing an integrator creation or update request.
/// Used to transfer integrator data from API to application layer.
/// </summary>
public class IntegratorRequestModel
{
    #region Properties

    /// <summary>
    /// Gets or sets the name of the integrator.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the integrator.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version of the integrator.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegratorRequestModel"/> class.
    /// </summary>
    public IntegratorRequestModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegratorRequestModel"/> class with specified values.
    /// </summary>
    /// <param name="name">The name of the integrator</param>
    /// <param name="description">The description of the integrator</param>
    /// <param name="version">The version of the integrator</param>
    public IntegratorRequestModel(string name, string description, string version)
    {
        Name = name;
        Description = description;
        Version = version;
    }

    #endregion
}

#endregion

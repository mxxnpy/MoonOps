#region Namespace Imports
#endregion

namespace MoonOps.Application.Models;

#region Integrator Response Model

/// <summary>
/// Model representing an integrator in API responses.
/// Used to transfer integrator data between layers.
/// </summary>
public class IntegratorResponseModel : BaseModel
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

    /// <summary>
    /// Gets or sets a value indicating whether the integrator is active.
    /// </summary>
    public bool IsActive { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegratorResponseModel"/> class.
    /// </summary>
    public IntegratorResponseModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegratorResponseModel"/> class with specified values.
    /// </summary>
    /// <param name="id">The unique identifier</param>
    /// <param name="name">The name of the integrator</param>
    /// <param name="description">The description of the integrator</param>
    /// <param name="version">The version of the integrator</param>
    /// <param name="isActive">Whether the integrator is active</param>
    /// <param name="createdAt">The creation timestamp</param>
    /// <param name="updatedAt">The last update timestamp</param>
    public IntegratorResponseModel(
        Guid id, 
        string name, 
        string description, 
        string version, 
        bool isActive,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Version = version;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    #endregion
}

#endregion

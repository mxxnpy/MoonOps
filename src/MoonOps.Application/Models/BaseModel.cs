#region Namespace Imports
#endregion

namespace MoonOps.Application.Models;

#region Base Model

/// <summary>
/// Base model class for data transfer between layers.
/// Provides common properties for all models in the application.
/// </summary>
public abstract class BaseModel
{
    #region Properties

    /// <summary>
    /// Gets or sets the unique identifier for the model.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Null if the entity has never been updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    #endregion
}

#endregion

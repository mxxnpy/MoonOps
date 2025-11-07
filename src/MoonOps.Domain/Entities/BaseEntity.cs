#region Namespace Imports
#endregion

namespace MoonOps.Domain.Entities;

#region Base Entity

/// <summary>
/// Base entity class with common properties for all domain entities.
/// Provides audit fields and identity management for domain objects.
/// </summary>
public abstract class BaseEntity
{
    #region Properties

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// Generated automatically upon entity creation.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// Set automatically upon entity creation.
    /// </summary>
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time when the entity was last updated.
    /// Null if the entity has never been updated since creation.
    /// </summary>
    public DateTime? UpdatedAt { get; protected set; }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Marks the entity as modified by updating the UpdatedAt timestamp.
    /// Should be called whenever the entity state changes.
    /// </summary>
    protected void MarkAsModified()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    #endregion
}

#endregion

#region Namespace Imports
using MoonOps.Domain.Entities;
#endregion

namespace MoonOps.Domain.Entities;

#region Integrator Entity

/// <summary>
/// Represents an integrator in the domain.
/// This is an example entity for central repository integration.
/// </summary>
public class Integrator : BaseEntity
{
    #region Properties

    /// <summary>
    /// Gets or sets the name of the integrator.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the integrator.
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version of the integrator.
    /// </summary>
    public string Version { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the integrator is active.
    /// </summary>
    public bool IsActive { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Integrator"/> class.
    /// Private constructor for ORM use.
    /// </summary>
    private Integrator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Integrator"/> class with the specified values.
    /// </summary>
    /// <param name="name">The name of the integrator</param>
    /// <param name="description">The description of the integrator</param>
    /// <param name="version">The version of the integrator</param>
    public Integrator(string name, string description, string version)
    {
        Name = name;
        Description = description;
        Version = version;
        IsActive = true;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the integrator information.
    /// </summary>
    /// <param name="name">The new name</param>
    /// <param name="description">The new description</param>
    /// <param name="version">The new version</param>
    public void Update(string name, string description, string version)
    {
        Name = name;
        Description = description;
        Version = version;
        MarkAsModified();
    }

    /// <summary>
    /// Activates the integrator.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        MarkAsModified();
    }

    /// <summary>
    /// Deactivates the integrator.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        MarkAsModified();
    }

    #endregion
}

#endregion

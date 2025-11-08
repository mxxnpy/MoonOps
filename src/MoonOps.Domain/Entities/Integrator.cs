using MoonOps.Domain.Entities;

namespace MoonOps.Domain.Entities;

public class Integrator : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private Integrator()
    {
    }

    public Integrator(string name, string description, string version)
    {
        Name = name;
        Description = description;
        Version = version;
        IsActive = true;
    }

    public void Update(string name, string description, string version)
    {
        Name = name;
        Description = description;
        Version = version;
        MarkAsModified();
    }

    public void Activate()
    {
        IsActive = true;
        MarkAsModified();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkAsModified();
    }
}

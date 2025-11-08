namespace MoonOps.Domain.Models;

public class HealthResponseModel
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Version { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public TimeSpan Uptime { get; set; }
    public string MachineName { get; set; } = string.Empty;
    public int ProcessorCount { get; set; }
    public long WorkingSet { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
}

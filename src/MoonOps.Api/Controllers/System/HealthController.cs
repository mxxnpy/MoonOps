using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoonOps.Domain.Models;

namespace MoonOps.Api.Controllers.System;

[ApiExplorerSettings(GroupName = "System")]
public class HealthController : BaseController
{
    [HttpGet("")]
    [ProducesResponseType(typeof(HealthResponseModel), StatusCodes.Status200OK)]
    public IActionResult GetHealth() =>
        Ok(new HealthResponseModel
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            Uptime = DateTime.UtcNow.Subtract(Process.GetCurrentProcess().StartTime.ToUniversalTime()),
            MachineName = Environment.MachineName,
            ProcessorCount = Environment.ProcessorCount,
            WorkingSet = Environment.WorkingSet,
            Author = "mxxnpy",
            Contact = "contato@moonops.dev"
        });
}
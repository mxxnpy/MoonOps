using Microsoft.AspNetCore.Mvc;
using MoonOps.Application.Services;
using MoonOps.Domain.Models;

namespace MoonOps.Api.Controllers.V1;

[ApiExplorerSettings(GroupName = "V1")]
public class IntegratorsController : BaseController
{
    private readonly IntegratorService _integratorService;

    public IntegratorsController(IntegratorService integratorService) => 
        _integratorService = integratorService;

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<IntegratorResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await _integratorService.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(IntegratorResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
     var integrator = await _integratorService.GetByIdAsync(id, ct);
        return integrator is null ? NotFound() : Ok(integrator);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(IntegratorResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] IntegratorRequestModel request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var integrator = await _integratorService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = integrator.Id }, integrator);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IntegratorResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] IntegratorRequestModel request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var integrator = await _integratorService.UpdateAsync(id, request, ct);
        return integrator is null ? NotFound() : Ok(integrator);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
var deleted = await _integratorService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
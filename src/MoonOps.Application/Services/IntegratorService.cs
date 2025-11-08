using MoonOps.Domain.Interfaces;
using MoonOps.Domain.Models;
using MoonOps.Domain.Entities;

namespace MoonOps.Application.Services;

public class IntegratorService : IApplicationService
{
    private readonly IRepository<Integrator> _integratorRepository;

    public IntegratorService(IRepository<Integrator> integratorRepository)
    {
        _integratorRepository = integratorRepository;
    }

    public async Task<IEnumerable<IntegratorResponseModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var integrators = await _integratorRepository.GetAllAsync(cancellationToken);
        return integrators.Select(MapToResponseModel);
    }

    public async Task<IntegratorResponseModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var integrator = await _integratorRepository.GetByIdAsync(id, cancellationToken);
        return integrator != null ? MapToResponseModel(integrator) : null;
    }

    public async Task<IntegratorResponseModel> CreateAsync(
        IntegratorRequestModel requestModel, 
      CancellationToken cancellationToken = default)
    {
        var integrator = new Integrator(
         requestModel.Name,
            requestModel.Description,
            requestModel.Version);

        var createdIntegrator = await _integratorRepository.AddAsync(integrator, cancellationToken);
        return MapToResponseModel(createdIntegrator);
  }

    public async Task<IntegratorResponseModel?> UpdateAsync(
      Guid id,
        IntegratorRequestModel requestModel,
        CancellationToken cancellationToken = default)
    {
      var integrator = await _integratorRepository.GetByIdAsync(id, cancellationToken);
        if (integrator == null)
        {
   return null;
        }

        integrator.Update(
       requestModel.Name,
            requestModel.Description,
  requestModel.Version);

        await _integratorRepository.UpdateAsync(integrator, cancellationToken);
        return MapToResponseModel(integrator);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var integrator = await _integratorRepository.GetByIdAsync(id, cancellationToken);
        if (integrator == null)
        {
        return false;
        }

        await _integratorRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    private static IntegratorResponseModel MapToResponseModel(Integrator integrator)
    {
 return new IntegratorResponseModel
        {
            Id = integrator.Id,
      CreatedAt = integrator.CreatedAt,
        UpdatedAt = integrator.UpdatedAt,
            Name = integrator.Name,
        Description = integrator.Description,
     Version = integrator.Version,
 IsActive = integrator.IsActive
        };
    }
}

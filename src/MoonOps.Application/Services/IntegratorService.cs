#region Namespace Imports
using MoonOps.Application.Interfaces;
using MoonOps.Application.Models;
using MoonOps.Domain.Entities;
using MoonOps.Domain.Interfaces;
#endregion

namespace MoonOps.Application.Services;

#region Integrator Service

/// <summary>
/// Application service for managing integrators.
/// Provides business logic orchestration for integrator operations.
/// </summary>
public class IntegratorService : IApplicationService
{
    #region Fields

    /// <summary>
    /// Repository for integrator entities.
    /// </summary>
    private readonly IRepository<Integrator> _integratorRepository;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegratorService"/> class.
    /// </summary>
    /// <param name="integratorRepository">The integrator repository</param>
    public IntegratorService(IRepository<Integrator> integratorRepository)
    {
        _integratorRepository = integratorRepository;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Retrieves all integrators.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>A collection of integrator response models</returns>
    public async Task<IEnumerable<IntegratorResponseModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var integrators = await _integratorRepository.GetAllAsync(cancellationToken);
        return integrators.Select(MapToResponseModel);
    }

    /// <summary>
    /// Retrieves an integrator by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the integrator</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>The integrator response model if found, otherwise null</returns>
    public async Task<IntegratorResponseModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var integrator = await _integratorRepository.GetByIdAsync(id, cancellationToken);
        return integrator != null ? MapToResponseModel(integrator) : null;
    }

    /// <summary>
    /// Creates a new integrator.
    /// </summary>
    /// <param name="requestModel">The integrator creation request</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>The created integrator response model</returns>
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

    /// <summary>
    /// Updates an existing integrator.
    /// </summary>
    /// <param name="id">The unique identifier of the integrator to update</param>
    /// <param name="requestModel">The integrator update request</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>The updated integrator response model if found, otherwise null</returns>
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

    /// <summary>
    /// Deletes an integrator by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the integrator to delete</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>True if the integrator was deleted, false if not found</returns>
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

    #endregion

    #region Private Methods

    /// <summary>
    /// Maps an integrator entity to a response model.
    /// </summary>
    /// <param name="integrator">The integrator entity</param>
    /// <returns>The integrator response model</returns>
    private static IntegratorResponseModel MapToResponseModel(Integrator integrator)
    {
        return new IntegratorResponseModel(
            integrator.Id,
            integrator.Name,
            integrator.Description,
            integrator.Version,
            integrator.IsActive,
            integrator.CreatedAt,
            integrator.UpdatedAt);
    }

    #endregion
}

#endregion

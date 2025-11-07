#region Namespace Imports
#endregion

namespace MoonOps.Domain.Interfaces;

#region Repository Interface

/// <summary>
/// Generic repository interface for domain entities.
/// Defines standard CRUD operations for entity persistence.
/// </summary>
/// <typeparam name="T">The entity type that the repository manages</typeparam>
public interface IRepository<T> where T : class
{
    #region Query Methods

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>The entity if found, otherwise null</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>A collection of all entities</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Command Methods

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>The added entity with any generated values</returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>A task representing the async operation</returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>A task representing the async operation</returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    #endregion
}

#endregion

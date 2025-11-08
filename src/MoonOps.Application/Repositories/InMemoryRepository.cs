#region Namespace Imports
using MoonOps.Domain.Interfaces;
#endregion

namespace MoonOps.Application.Repositories;

#region In Memory Repository

/// <summary>
/// Implementação temporária em memória do repositório para desenvolvimento.
/// Autor: mxxnpy | Email: contato@moonops.dev
/// Esta implementação deve ser substituída por persistência real posteriormente.
/// </summary>
/// <typeparam name="T">Tipo da entidade gerenciada pelo repositório</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class
{
    #region Fields

    /// <summary>
    /// Armazenamento em memória dos dados.
    /// </summary>
    private readonly List<T> _data = new();
    
    /// <summary>
    /// Semáforo para operações thread-safe.
    /// </summary>
    private readonly SemaphoreSlim _semaphore = new(1, 1);

  #endregion

    #region Query Methods

 /// <summary>
    /// Obtém uma entidade pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da entidade</param>
  /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A entidade se encontrada, senão null</returns>
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
   await _semaphore.WaitAsync(cancellationToken);
        try
      {
    // Implementação simplificada - assumindo que T tem uma propriedade Id
          return _data.FirstOrDefault(x => GetEntityId(x) == id);
        }
        finally
        {
        _semaphore.Release();
    }
 }

    /// <summary>
    /// Obtém todas as entidades do repositório.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Coleção de todas as entidades</returns>
    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
        return _data.ToList();
   }
        finally
        {
  _semaphore.Release();
        }
    }

    #endregion

 #region Command Methods

 /// <summary>
    /// Adiciona uma nova entidade ao repositório.
    /// </summary>
 /// <param name="entity">Entidade a ser adicionada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A entidade adicionada</returns>
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
      try
  {
    _data.Add(entity);
         return entity;
        }
 finally
        {
         _semaphore.Release();
      }
    }

    /// <summary>
    /// Atualiza uma entidade existente no repositório.
    /// </summary>
 /// <param name="entity">Entidade a ser atualizada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
try
   {
            var id = GetEntityId(entity);
            var existingIndex = _data.FindIndex(x => GetEntityId(x) == id);
    
            if (existingIndex >= 0)
        {
 _data[existingIndex] = entity;
     }
        }
  finally
        {
      _semaphore.Release();
      }
    }

    /// <summary>
    /// Remove uma entidade do repositório pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador único da entidade a ser removida</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
    await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var item = _data.FirstOrDefault(x => GetEntityId(x) == id);
      if (item != null)
    {
         _data.Remove(item);
     }
      }
     finally
        {
 _semaphore.Release();
        }
 }

    #endregion

    #region Private Methods

    /// <summary>
    /// Obtém o ID da entidade usando reflexão.
    /// Método temporário até implementação de persistência real.
    /// </summary>
    /// <param name="entity">Entidade</param>
    /// <returns>ID da entidade</returns>
    private static Guid GetEntityId(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
      return idProperty?.GetValue(entity) as Guid? ?? Guid.Empty;
    }

    #endregion
}

#endregion
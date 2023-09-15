using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class, IDynamoDbModel
    {
        Task SaveAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}

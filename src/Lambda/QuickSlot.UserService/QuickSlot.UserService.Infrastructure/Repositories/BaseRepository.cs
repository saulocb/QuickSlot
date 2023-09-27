using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using QuickSlot.UserService.Infrastructure.Interfaces;

namespace QuickSlot.UserService.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IDynamoDbModel
    {
        private readonly IDynamoDBContext _context;
        private readonly DynamoDBOperationConfig _config;

        public BaseRepository(IDynamoDBContext context, DynamoDBOperationConfig configFactory)
        {
            _context = context;
            _config = configFactory;
        }

        public async Task SaveAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveAsync(entity, _config, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.DeleteAsync(entity, _config, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var search = _context.ScanAsync<T>(new List<ScanCondition>(), _config);
            return await search.GetNextSetAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _context.LoadAsync<T>(id, _config, cancellationToken);
        }
    }
}

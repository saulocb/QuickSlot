using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDynamoDBContext _context;

        public BaseRepository(IAmazonDynamoDB client)
        {
            _context = new DynamoDBContext(client);
        }

        public async Task SaveAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.SaveAsync(entity, cancellationToken);
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _context.LoadAsync<T>(id, cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.DeleteAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var search = _context.ScanAsync<T>(new List<ScanCondition>());
            return await search.GetNextSetAsync(cancellationToken);
        }
    }
}

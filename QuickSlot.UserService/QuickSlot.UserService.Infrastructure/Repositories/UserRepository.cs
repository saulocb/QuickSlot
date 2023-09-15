using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using QuickSlot.UserService.Infrastructure.Factories;
using QuickSlot.UserService.Infrastructure.Interfaces;


namespace QuickSlot.UserService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDynamoDBContext context, DynamoDbOperationConfigFactory configFactory, string tableName)
            : base(context, configFactory.CreateConfigFor(tableName))
        {
        }
    }
}

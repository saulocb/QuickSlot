using Amazon.DynamoDBv2;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IAmazonDynamoDB client) : base(client)
        {
        }
    }
}

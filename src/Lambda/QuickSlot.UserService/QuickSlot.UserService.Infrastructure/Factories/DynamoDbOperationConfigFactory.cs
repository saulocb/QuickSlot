using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Infrastructure.Factories
{
    public class DynamoDbOperationConfigFactory : IDynamoDbOperationConfigFactory
    {
        public DynamoDBOperationConfig CreateConfigFor(string tableName)
        {
            return new DynamoDBOperationConfig
            {
                OverrideTableName = tableName,
            };
        }
    }
}

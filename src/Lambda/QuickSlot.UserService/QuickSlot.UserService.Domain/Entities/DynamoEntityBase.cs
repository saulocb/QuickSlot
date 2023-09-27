using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.Entities
{
    public abstract class DynamoEntityBase : IDynamoDbModel
    {
        [DynamoDBHashKey]
        public string? PK { get; set; } 

        [DynamoDBRangeKey]
        public string? SK { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty]
        public DateTime UpdatedAt { get; set; }
    }
}

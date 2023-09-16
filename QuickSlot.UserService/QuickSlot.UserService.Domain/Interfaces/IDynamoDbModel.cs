using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.Interfaces
{
    public interface IDynamoDbModel
    {
        [DynamoDBHashKey]
        string? PK { get; set; }

        [DynamoDBRangeKey]
        string? SK { get; set; }
    }

}

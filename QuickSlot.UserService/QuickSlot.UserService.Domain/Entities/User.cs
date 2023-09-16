using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.Entities
{
    [DynamoDBTable("UserTable")]
    public class User : DynamoEntityBase
    {
        [DynamoDBProperty]
        public string? Name { get; set; }

        [DynamoDBProperty]
        public UserType UserType { get; set; }

        [DynamoDBProperty]
        public string? Sub { get; set; }

        [DynamoDBProperty]
        public string? Email { get; set; }
    }

}

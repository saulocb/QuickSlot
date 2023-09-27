using Amazon.DynamoDBv2.DataModel;
using QuickSlot.UserService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.Entities
{
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

        [DynamoDBProperty]
        public Address? Address { get; private set; }

        [DynamoDBProperty]
        public Contact? Contact { get; private set; }

        [DynamoDBProperty]
        public BillPaymentMethod? BillPaymentMethod { get; private set; }

        public void UpdateAddress(Address newAddress)
        {
            Address = newAddress;
        }

        public void UpdateContact(Contact newContact)
        {
            Contact = newContact;
        }

        public void UpdateBillPaymentMethod(BillPaymentMethod newMethod)
        {
            BillPaymentMethod = newMethod;
        }
    }

}

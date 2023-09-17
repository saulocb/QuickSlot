using MediatR;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.DomainEvents
{
    public class UserBillPaymentMethodUpdated: INotification, IDomainEvent
    {
        public UserBillPaymentMethodUpdated(BillPaymentMethod newUserBillPaymentMethodUpdated)
        {
            NewUserBillPaymentMethodUpdated=newUserBillPaymentMethodUpdated;
        }

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public BillPaymentMethod NewUserBillPaymentMethodUpdated { get; }


    }
}

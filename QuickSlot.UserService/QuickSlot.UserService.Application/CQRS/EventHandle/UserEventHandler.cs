using MediatR;
using QuickSlot.UserService.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.EventHandle
{
    public class UserEventHandler : INotificationHandler<UserAddressUpdated>,
                                    INotificationHandler<UserBillPaymentMethodUpdated>
    {
        public Task Handle(UserAddressUpdated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(UserBillPaymentMethodUpdated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}

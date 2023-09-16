using MediatR;
using QuickSlot.UserService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Commands
{
    public class UpdateUserBillPaymentMethodCommand : IRequest<bool>
    {
        /// <summary>
        /// this is the User PK 
        /// </summary>
        public string PK { get; set; }
        public BillPaymentMethodDTO NewBillPaymentMethod { get; set; }
    }
}

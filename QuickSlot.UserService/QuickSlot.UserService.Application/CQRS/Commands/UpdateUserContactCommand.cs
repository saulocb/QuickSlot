using MediatR;
using QuickSlot.UserService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Commands
{
    public class UpdateUserContactCommand : IRequest<bool>
    {
        /// <summary>
        /// this is the user PK
        /// </summary>
        public string PK { get; set; }
        public ContactDTO NewContact { get; set; }
    }
}

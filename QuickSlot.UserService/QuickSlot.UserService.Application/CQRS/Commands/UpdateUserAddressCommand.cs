using MediatR;
using QuickSlot.UserService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Commands
{
    public class UpdateUserAddressCommand : IRequest<bool>
    {
        public string? PK { get; set; }

        public AddressDTO? NewAddress { get; set; }
    }
}

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
        /// <summary>
        /// this is the user PK 
        /// </summary>
        [Required(ErrorMessage = "PK cannot be null")]
        [MinLength(1, ErrorMessage = "PK cannot be empty")]
        public string? PK { get; set; }

        [Required(ErrorMessage = "NewAddress cannot be null")]
        public AddressDTO? NewAddress { get; set; }
    }
}

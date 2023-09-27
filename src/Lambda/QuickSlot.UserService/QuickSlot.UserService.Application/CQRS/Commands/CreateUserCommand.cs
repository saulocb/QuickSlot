using MediatR;
using Newtonsoft.Json.Converters;
using QuickSlot.UserService.Shared.DTOs;
using QuickSlot.UserService.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        public CreateUserCommand(string? sub, string? email, UserTypeDTO userType)
        {
            Sub = sub;
            Email = email;
            UserType=userType;
        }
        public string? Sub { get; set; }

        public string? Email { get; set; }

        public UserTypeDTO UserType { get; set; }
    }
}

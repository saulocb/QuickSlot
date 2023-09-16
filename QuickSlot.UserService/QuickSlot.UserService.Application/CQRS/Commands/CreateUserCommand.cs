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

        [Required(ErrorMessage = "Sub cannot be null")]
        [MinLength(1, ErrorMessage = "Sub cannot be empty")]
        public string? Sub { get; set; }

        [Required(ErrorMessage = "Email cannot be null")]
        [MinLength(1, ErrorMessage = "Email cannot be empty")]
        [MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "UserType cannot be null")]
        public UserTypeDTO UserType { get; set; }
    }
}

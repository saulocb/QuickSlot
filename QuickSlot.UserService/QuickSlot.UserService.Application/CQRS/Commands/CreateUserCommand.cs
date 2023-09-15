using QuickSlot.UserService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Commands
{
    public class CreateUserCommand
    {
        public UserDTO UserDto { get; }

        public CreateUserCommand(UserDTO userDto)
        {
            UserDto = userDto;
        }
    }
}

using QuickSlot.UserService.Shared.DTOs;

namespace QuickSlot.UserService.Application.CQRS.Commands
{
    public class UpdateUserCommand
    {
        public int Id { get; }
        public UserDTO UserDto { get; }

        public UpdateUserCommand(int id, UserDTO userDto)
        {
            Id = id;
            UserDto = userDto;
        }
    }
}

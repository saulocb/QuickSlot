using MediatR;
using QuickSlot.UserService.Application.CQRS.Commands;
using QuickSlot.UserService.Application.CQRS.Queries;
using QuickSlot.UserService.Shared.DTOs;

namespace QuickSlot.UserService.Application.CQRS.Mediators
{
    public class UserMediator
    {
        private readonly IMediator _mediator;

        public UserMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void CreateUser(UserDTO userDto)
        {
            var command = new CreateUserCommand(userDto);
            _mediator.Send(command);
        }

        public void UpdateUser(int id, UserDTO userDto)
        {
            var command = new UpdateUserCommand(id, userDto);
            _mediator.Send(command);
        }

        public void DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);
            _mediator.Send(command);
        }

        public UserDTO GetUser(int id)
        {
            var query = new GetUserQuery(id);
            return (UserDTO)_mediator.Send(query).Result;
        }

        public List<UserDTO> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            return (List<UserDTO>)_mediator.Send(query).Result;
        }
    }
}

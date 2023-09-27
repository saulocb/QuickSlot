using MediatR;
using QuickSlot.UserService.Domain.Interfaces;
using QuickSlot.UserService.Shared.DTOs;

namespace QuickSlot.UserService.Application.CQRS.Queries.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;  

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // Fetch user from the database and map it to UserDTO
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return null; // Or throw a UserNotFoundException
            }

            // Map User entity to UserDTO (assuming you have a mapper)
            return new UserDTO
            {
              Email = user.Email,
              Sub =  user.Sub,

            };
        }
    }
}

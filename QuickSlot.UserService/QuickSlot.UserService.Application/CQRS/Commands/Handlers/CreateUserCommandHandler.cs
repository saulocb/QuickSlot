using MediatR;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Commands.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                PK = Guid.NewGuid().ToString(),
                SK = Guid.NewGuid().ToString(),
                Email = request.Email,
            };

            await _userRepository.SaveAsync(user, cancellationToken);

            return user.PK;
        }
    }
}

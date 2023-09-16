using AutoMapper;
using MediatR;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace QuickSlot.UserService.Application.CQRS.Commands.Handlers
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger=logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request);
                await _userRepository.SaveAsync(user, cancellationToken);

                return user.PK;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

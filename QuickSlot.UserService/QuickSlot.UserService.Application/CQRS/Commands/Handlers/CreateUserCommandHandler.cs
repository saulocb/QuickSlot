using AutoMapper;
using MediatR;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using QuickSlot.UserService.Application.Exceptions;
using QuickSlot.UserService.Domain.DomainEvents;

namespace QuickSlot.UserService.Application.CQRS.Commands.Handlers
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>,
                                            IRequestHandler<UpdateUserAddressCommand, bool>,
                                            IRequestHandler<UpdateUserContactCommand, bool>,
                                            IRequestHandler<UpdateUserBillPaymentMethodCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<CreateUserCommandHandler> logger, IMediator mediator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger=logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator=mediator ?? throw new ArgumentNullException(nameof(mediator));
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

        public async Task<bool> Handle(UpdateUserAddressCommand? request, CancellationToken cancellationToken)
        {
            try
            {
                // fetch the user 
                User? user = await _userRepository.GetByIdAsync(request.PK, cancellationToken);
                if (user == null)
                    throw new UserNotFoundException($"Error fetching user by ID: {request.PK}");

                // update user in memory
                var newAddress = _mapper.Map<Address>(request.NewAddress);
                user.UpdateAddress(newAddress);

                // save update
                await _userRepository.SaveAsync(user, cancellationToken);

                // Publish the domain event
                await _mediator.Publish(new UserAddressUpdated(newAddress), cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> Handle(UpdateUserContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // fetch the user 
                User? user = await _userRepository.GetByIdAsync(request.PK, cancellationToken);
                if (user == null)
                    throw new UserNotFoundException($"Error fetching user by ID: {request.PK}");

                // update user in memory
                var newContact = _mapper.Map<Contact>(request.NewContact);
                user.UpdateContact(newContact);

                // save update
                await _userRepository.SaveAsync(user, cancellationToken);
                return true;
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> Handle(UpdateUserBillPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // fetch the user 
                User? user = await _userRepository.GetByIdAsync(request.PK, cancellationToken);
                if (user == null)
                    throw new UserNotFoundException($"Error fetching user by ID: {request.PK}");

                // update user in memory
                var newBillPaymentMethod = _mapper.Map<BillPaymentMethod>(request.NewBillPaymentMethod);
                user.UpdateBillPaymentMethod(newBillPaymentMethod);

                // save update
                await _userRepository.SaveAsync(user, cancellationToken);

                // Publish the domain event
                await _mediator.Publish(new UserBillPaymentMethodUpdated(newBillPaymentMethod), cancellationToken);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

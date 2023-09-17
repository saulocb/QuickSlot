using FluentValidation;
using QuickSlot.UserService.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.Validation
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // Validate Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format")
                .NotNull().WithMessage("Email cannot be null")
                .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters");

            // Validate Sub
            RuleFor(x => x.Sub)
                .NotEmpty().WithMessage("Sub cannot be empty")
                .NotNull().WithMessage("Sub cannot be null")
                .MinimumLength(1).WithMessage("Sub must be at least 1 character long");

            // Validate UserType
            RuleFor(x => x.UserType)
                .IsInEnum().WithMessage("Invalid user type")
                .NotNull().WithMessage("UserType cannot be null")
                .NotEmpty();
        }
    }

}

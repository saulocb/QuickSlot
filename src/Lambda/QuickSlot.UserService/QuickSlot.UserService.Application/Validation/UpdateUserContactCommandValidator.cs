using FluentValidation;
using QuickSlot.UserService.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.Validation
{
    internal class UpdateUserContactCommandValidator : AbstractValidator<UpdateUserContactCommand>
    {
        public UpdateUserContactCommandValidator()
        {
            // Validate Email
            RuleFor(x => x.PK)
                .NotEmpty().WithMessage("PK cannot be empty")
                .NotNull().WithMessage("PK cannot be null");

            // Validate Sub
            RuleFor(x => x.NewContact.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.NewContact.PhoneNumber).NotEmpty().NotNull();
        }
    }
}

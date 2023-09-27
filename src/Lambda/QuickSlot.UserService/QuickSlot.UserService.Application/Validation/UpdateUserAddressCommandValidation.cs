using FluentValidation;
using QuickSlot.UserService.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.Validation
{
    internal class UpdateUserAddressCommandValidation : AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressCommandValidation()
        {
            // Validate Email
            RuleFor(x => x.PK)
                .NotEmpty().WithMessage("PK cannot be empty")
                .NotNull().WithMessage("PK cannot be null");

            // Validate Sub
            RuleFor(x => x.NewAddress.City).NotEmpty().NotNull();
            RuleFor(x => x.NewAddress.Street).NotEmpty().NotNull();
            RuleFor(x => x.NewAddress.State).NotEmpty().NotNull();
        }
    }
}

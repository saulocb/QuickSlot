using FluentValidation;
using QuickSlot.UserService.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.Validation
{
    internal class UpdateUserBillPaymentMethodCommandValidator : AbstractValidator<UpdateUserBillPaymentMethodCommand>
    {
        public UpdateUserBillPaymentMethodCommandValidator()
        {
            // Validate Email
            RuleFor(x => x.PK)
                .NotEmpty().WithMessage("PK cannot be empty")
                .NotNull().WithMessage("PK cannot be null");

            // Validate Sub
            RuleFor(x => x.NewBillPaymentMethod.CVV).NotEmpty().NotNull();
            RuleFor(x => x.NewBillPaymentMethod.CardNumber).NotEmpty().NotNull();
            RuleFor(x => x.NewBillPaymentMethod.ExpiryDate).NotEmpty().NotNull();
        }
    }
}

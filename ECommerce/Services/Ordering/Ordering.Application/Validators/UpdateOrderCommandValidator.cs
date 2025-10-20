using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.IValidators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
            RuleFor(v => v.Username)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");
            RuleFor(v => v.EmailAddress)
                .NotEmpty().WithMessage("EmailAddress is required.")
                .EmailAddress().WithMessage("A valid email is required.")
                .MaximumLength(100).WithMessage("EmailAddress must not exceed 100 characters.");
            RuleFor(v => v.TotalPrice)
                .GreaterThan(0).WithMessage("TotalPrice must be greater than zero.");
        }
    }
}

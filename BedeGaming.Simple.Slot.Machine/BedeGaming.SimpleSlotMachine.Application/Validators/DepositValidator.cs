using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using FluentValidation;

namespace BedeGaming.SimpleSlotMachine.Application.Validators
{
    public class DepositValidator : AbstractValidator<double>, IDepositValidator
    {
        public DepositValidator()
        {
            RuleFor(deposit => deposit).GreaterThan(0).WithMessage(Messages.Balance.DepositAmountShouldBeGreaterThanZero);
        }
    }
}

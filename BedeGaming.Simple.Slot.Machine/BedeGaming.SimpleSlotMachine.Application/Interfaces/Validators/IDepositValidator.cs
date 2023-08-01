using FluentValidation;

namespace BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators
{
    public interface IDepositValidator : IValidator<double>
    {
    }
}

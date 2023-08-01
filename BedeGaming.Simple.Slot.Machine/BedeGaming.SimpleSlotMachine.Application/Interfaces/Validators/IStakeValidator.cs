using FluentValidation;

namespace BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators
{
    public interface IStakeValidator : IValidator<decimal>
    {
    }
}

using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Extensions;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using FluentValidation;

namespace BedeGaming.SimpleSlotMachine.Application.Validators
{
    public class StakeValidator : AbstractValidator<double>, IStakeValidator
    {
        public StakeValidator()
        {
            RuleFor(stake => stake).GreaterThan(0).WithMessage(Messages.Balance.StakeAmountShouldBeGreaterThanZero)
                 .Must((stake, value, context) =>
                 {
                     var slotMachineService = context.GetSlotMachineService();
                     var balance = slotMachineService.Balance;
                     Console.WriteLine(balance);
                     Console.WriteLine(stake);
                     return stake <= balance;
                 })
            .WithMessage(Messages.Balance.StakeCannotBeGraterThanBalance);
        }
    }
}

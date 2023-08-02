using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using Consoles.Common.Interfaces;
using FluentValidation.Results;

namespace BedeGaming.SimpleSlotMachine.Application.Providers
{
    public class InitialBalanceProvider : IInitialBalanceProvider
    {
        private readonly IDepositValidator _validator;
        private readonly IConsoleInputReader _consoleInputReader;
        private decimal _deposit;

        public InitialBalanceProvider(IDepositValidator validator, IConsoleInputReader consoleInputReader)
        {
            _validator = validator;
            _consoleInputReader = consoleInputReader;
        }

        public decimal Deposit
        {
            get => _deposit;
            set
            {
                ValidationResult result = _validator.Validate(value);

                while (!result.IsValid)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }

                    value = Math.Round(_consoleInputReader.ReadValidInput<decimal>(Messages.Balance.InitialDepositPrompt), 2);
                    result = _validator.Validate(value);
                }

                _deposit = value;
            }
        }
    }
}

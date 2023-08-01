using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using Consoles.Common.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace BedeGaming.SimpleSlotMachine.Application.Providers
{
    public class InitialBalanceProvider : IInitialBalanceProvider
    {
        private readonly IDepositValidator _validator;
        private readonly IConsoleInputReader _consoleInputReader;
        private double _deposit;

        public InitialBalanceProvider(IDepositValidator validator, IConsoleInputReader consoleInputReader)
        {
            _validator = validator;
            _consoleInputReader = consoleInputReader;
        }

        public double Deposit
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

                    value = _consoleInputReader.ReadValidInput<double>(Messages.Balance.InitialDepositPrompt);

                    result = _validator.Validate(value);
                }

                _deposit = value;
            }
        }
    }
}

using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using Consoles.Common.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace BedeGaming.SimpleSlotMachine.Application.Providers
{
    public class InitialBalanceProvider : IInitialBalanceProvider
    {
        private readonly IValidator<double> _validator;
        private readonly IConsoleInputReader _consoleInputReader;
        private double _deposit;

        public InitialBalanceProvider(IValidator<double> validator, IConsoleInputReader consoleInputReader)
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

                    value = _consoleInputReader.ReadValidInput<double>(Constant.Balance.InitialDeposit);

                    result = _validator.Validate(value);
                }

                _deposit = value;
            }
        }
    }
}

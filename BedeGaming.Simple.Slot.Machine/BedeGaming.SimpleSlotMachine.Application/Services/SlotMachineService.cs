using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Extensions;
using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using BedeGaming.SimpleSlotMachine.Domain;
using Consoles.Common.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace BedeGaming.SimpleSlotMachine.Application.Services
{
    public class SlotMachineService : ISlotMachineService
    {
        private readonly List<Symbol> _symbols;
        private readonly IInitialBalanceProvider _initialBalanceProvider;
        private readonly ISymbolGeneratorService _symbolGeneratorService;
        private readonly IConsoleInputReader _consoleInputReader;
        private readonly IStakeValidator _validator;

        public SlotMachineService(
            IInitialBalanceProvider initialBalanceProvider,
            ISymbolGeneratorService symbolGenerator,
            IConsoleInputReader consoleInputReader,
            IStakeValidator validator)
        {
            _initialBalanceProvider = initialBalanceProvider;
            _symbolGeneratorService = symbolGenerator;
            _consoleInputReader = consoleInputReader;
            _validator = validator;

            Balance = _initialBalanceProvider.Deposit;
            _symbols = _symbolGeneratorService.Symbols;
        }

        public double Balance { get; private set; }

        public void Play(double stakeAmount)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Messages.SlotMachine.SpinResults);
            Console.ResetColor();

            string[,] spinResult = new string[4, 3];

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Symbol randomSymbol = _symbolGeneratorService.GetRandomSymbol();
                    spinResult[row, col] = randomSymbol.Name;
                }
            }

            DisplaySpinResult(spinResult);
            PromptingForValidStake(stakeAmount);

            double winAmount = CalculateWinAmount(spinResult, stakeAmount);
            Balance = Balance - stakeAmount + winAmount;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Messages.SlotMachine.YouWin(winAmount, Balance));
            Console.ResetColor();

            if (Balance <= 0)
            {
                Console.WriteLine(Messages.SlotMachine.GameOver);
                return;
            }

            stakeAmount = _consoleInputReader.ReadValidInput<double>(Messages.SlotMachine.StakeAmountPrompt);
            Play(stakeAmount); // Play the next round
        }

        private void DisplaySpinResult(string[,] spinResult)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    string symbolName = spinResult[row, col];
                    Symbol symbol = _symbols!.Find(s => s.Name == symbolName);

                    // Get the color corresponding to the symbol
                    ConsoleColor symbolColor = symbol!.Color;

                    // Change the text color temporarily and display the symbol
                    Console.ForegroundColor = symbolColor;
                    Console.Write(symbolName);

                    // Reset the text color to the default
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        private double CalculateWinAmount(string[,] spinResult, double stakeAmount)
        {
            double winAmount = 0;

            // Check for horizontal win combinations
            for (int row = 0; row < 4; row++)
            {
                if (spinResult[row, 0] == spinResult[row, 1] && spinResult[row, 1] == spinResult[row, 2])
                {
                    Symbol symbol = _symbols.Find(s => s.Name == spinResult[row, 0]);
                    winAmount += symbol.Coefficient * stakeAmount;
                }
                else if (spinResult[row, 0] == "*" && spinResult[row, 1] == spinResult[row, 2])
                {
                    Symbol symbol = _symbols.Find(s => s.Name == spinResult[row, 1]);
                    winAmount += symbol.Coefficient * stakeAmount;
                }
                else if (spinResult[row, 2] == "*" && spinResult[row, 0] == spinResult[row, 1])
                {
                    Symbol symbol = _symbols.Find(s => s.Name == spinResult[row, 0]);
                    winAmount += symbol.Coefficient * stakeAmount;
                }
            }

            return winAmount;
        }

        private void PromptingForValidStake(double stake)
        {
            var validationContext = new ValidationContext<double>(stake);
            validationContext.SetSlotMachineService(this);
            ValidationResult result = _validator.Validate(validationContext);

            while (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                double newStake = _consoleInputReader.ReadValidInput<double>(Messages.SlotMachine.StakeAmountPrompt);
                validationContext = new ValidationContext<double>(newStake);
                validationContext.SetSlotMachineService(this);
                result = _validator.Validate(validationContext);
            }
        }
    }
}
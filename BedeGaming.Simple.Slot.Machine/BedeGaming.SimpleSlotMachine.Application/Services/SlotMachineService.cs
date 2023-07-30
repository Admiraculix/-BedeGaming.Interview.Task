using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Domain;

namespace BedeGaming.SimpleSlotMachine.Application.Services
{
    public class SlotMachineService : ISlotMachineService
    {
        private readonly List<Symbol> _symbols;
        private readonly IInitialBalanceProvider _initialBalanceProvider;
        private readonly ISymbolGeneratorService _symbolGeneratorService;
        private int _balance;

        public SlotMachineService(IInitialBalanceProvider initialBalanceProvider, ISymbolGeneratorService symbolGenerator)
        {
            _initialBalanceProvider = initialBalanceProvider;
            _balance = _initialBalanceProvider.GetInitialBalance();
            _symbolGeneratorService = symbolGenerator;
            _symbols = _symbolGeneratorService.Symbols;
        }

        public void Play(int stakeAmount)
        {
            if (_balance <= 0)
            {
                Console.WriteLine("\nGame over! You have no balance left. Thank you for playing!");
                return;
            }

            Console.WriteLine("\nSpin Result:");
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

            double winAmount = CalculateWinAmount(spinResult, stakeAmount);
            _balance = _balance - stakeAmount + (int)winAmount;

            Console.WriteLine($"You won: {winAmount}, Current balance: {_balance}");

            Play(stakeAmount); // Play the next round
        }

        private void DisplaySpinResult(string[,] spinResult)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(spinResult[row, col]);
                }
                Console.WriteLine();
            }
        }

        private double CalculateWinAmount(string[,] spinResult, int stakeAmount)
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
    }

}
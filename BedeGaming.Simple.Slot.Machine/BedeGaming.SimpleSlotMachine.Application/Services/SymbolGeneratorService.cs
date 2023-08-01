using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Domain;

namespace BedeGaming.SimpleSlotMachine.Application.Services
{
    public class SymbolGeneratorService : ISymbolGeneratorService
    {
        private readonly Random random;
        private readonly int totalProbability;

        public SymbolGeneratorService(List<Symbol> symbols)
        {
            random = new Random();
            Symbols = symbols;

            foreach (Symbol symbol in symbols)
            {
                totalProbability += symbol.Probability;
            }
        }

        public List<Symbol> Symbols { get; }

        public Symbol GetRandomSymbol()
        {
            int randomValue = random.Next(1, totalProbability + 1);
            int cumulativeProbability = 0;

            foreach (Symbol symbol in Symbols)
            {
                cumulativeProbability += symbol.Probability;
                if (randomValue <= cumulativeProbability)
                {
                    //Console.WriteLine($"CP: {cumulativeProbability},  Ran: {randomValue}");
                    return symbol;
                }
            }

            return null; // Default symbol (should never reach here)
        }

    }
}

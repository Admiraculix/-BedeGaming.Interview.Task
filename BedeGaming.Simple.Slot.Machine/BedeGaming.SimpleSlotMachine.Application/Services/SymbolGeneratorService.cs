using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Domain;

namespace BedeGaming.SimpleSlotMachine.Application.Services
{
    public class SymbolGeneratorService : ISymbolGeneratorService
    {
        private readonly Random random;
        private int totalProbability;

        public SymbolGeneratorService(List<Symbol> symbols)
        {
            random = new Random();
            Symbols = symbols;

            foreach (var symbol in symbols)
            {
                totalProbability += symbol.Probability;
            }
        }

        public List<Symbol> Symbols { get; }

        public Symbol GetRandomSymbol()
        {
            int randomValue = random.Next(1, totalProbability + 1);
            int cumulativeProbability = 0;

            foreach (var symbol in Symbols)
            {
                cumulativeProbability += symbol.Probability;
                if (randomValue <= cumulativeProbability)
                {
                    return symbol;
                }
            }

            return null; // Default symbol (should never reach here)
        }

    }
}

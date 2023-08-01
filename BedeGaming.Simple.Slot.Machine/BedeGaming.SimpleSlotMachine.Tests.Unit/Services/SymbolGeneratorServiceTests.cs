using BedeGaming.SimpleSlotMachine.Application.Services;
using BedeGaming.SimpleSlotMachine.Domain;
using FluentAssertions;

namespace BedeGaming.SimpleSlotMachine.Tests.Unit.Services
{
    public class SymbolGeneratorServiceTests
    {
        private readonly List<Symbol> _symbols;

        public SymbolGeneratorServiceTests()
        {
            _symbols = new List<Symbol>
            {
                new Symbol { Name = "A", Probability = 4 },
                new Symbol { Name = "B", Probability = 3 },
                new Symbol { Name = "P", Probability = 2 },
                new Symbol { Name = "*", Probability = 1 },
            };
        }

        [Fact]
        public void GetRandomSymbol_ShouldReturnNonNullSymbol()
        {
            // Arrange
            var symbolGeneratorService = new SymbolGeneratorService(_symbols);

            // Act
            var randomSymbol = symbolGeneratorService.GetRandomSymbol();

            // Assert
            randomSymbol.Should().NotBeNull();
        }

        [Fact]
        public void GetRandomSymbol_ShouldReturnSymbolsWithCorrectProbabilities()
        {
            // Arrange
            var symbolGeneratorService = new SymbolGeneratorService(_symbols);
            var totalProbability = _symbols.Sum(symbol => symbol.Probability);
            var iterations = 500000; // Increase the number of iterations for better statistical significance
            var tolerance = 0.001; // Allow a small difference due to rounding

            var expectedProbabilities = _symbols.ToDictionary(s => s.Name, s => (double)s.Probability / totalProbability);

            var occurrences = new Dictionary<string, int>();
            foreach (var symbol in _symbols)
            {
                occurrences[symbol.Name] = 0;
            }

            // Act
            for (int i = 0; i < iterations; i++)
            {
                var randomSymbol = symbolGeneratorService.GetRandomSymbol();
                occurrences[randomSymbol.Name]++;
            }

            // Assert
            foreach (var symbol in _symbols)
            {
                var actualProbability = (double)occurrences[symbol.Name] / iterations;
                var expectedProbability = expectedProbabilities[symbol.Name];

                actualProbability.Should().BeApproximately(expectedProbability, tolerance);
            }
        }
    }
}

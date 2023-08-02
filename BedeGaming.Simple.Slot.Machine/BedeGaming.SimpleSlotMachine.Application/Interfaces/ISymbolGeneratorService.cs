using BedeGaming.SimpleSlotMachine.Domain;

namespace BedeGaming.SimpleSlotMachine.Application.Interfaces
{
    public interface ISymbolGeneratorService
    {
        List<Symbol> Symbols { get; }

        /// <summary>
        /// Method uses 'Weighted Random Selection' algorithm
        /// </summary>
        /// <returns>Random symbol</returns>
        Symbol GetRandomSymbol();
    }
}

using BedeGaming.SimpleSlotMachine.Domain;

namespace BedeGaming.SimpleSlotMachine.Application.Interfaces
{
    public interface ISymbolGeneratorService
    {
        List<Symbol> Symbols { get; }
        Symbol GetRandomSymbol();
    }
}

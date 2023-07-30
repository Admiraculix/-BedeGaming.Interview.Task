using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Application.Providers;
using BedeGaming.SimpleSlotMachine.Application.Services;
using BedeGaming.SimpleSlotMachine.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BedeGaming.SimpleSlotMachine.ConsoleGame.Configurations
{
    public static class DependencyConfig
    {
        public static ServiceProvider ConfigureDependencies(int deposit)
        {
            var configuration = new ConfigurationBuilder()
                      .AddJsonFile(Path.Combine("Configurations", "appsettings.json"), optional: false, reloadOnChange: true)
                      .Build();

            var symbolsConfig = configuration.GetSection("Symbols").Get<List<Symbol>>();

            var serviceProvider = new ServiceCollection()
            .AddScoped<IInitialBalanceProvider, InitialBalanceProvider>(provider =>
            new InitialBalanceProvider(deposit))
            .AddScoped<ISymbolGeneratorService, SymbolGeneratorService>(provider =>
                new SymbolGeneratorService(symbolsConfig))
            .AddScoped<ISlotMachineService, SlotMachineService>()
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}

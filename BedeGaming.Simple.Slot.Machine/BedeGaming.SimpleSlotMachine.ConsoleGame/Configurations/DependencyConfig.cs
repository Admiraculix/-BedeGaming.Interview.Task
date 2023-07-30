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
            IConfigurationRoot configuration = new ConfigurationBuilder()
                      .AddJsonFile(Path.Combine("Configurations", "appsettings.json"), optional: false, reloadOnChange: true)
                      .Build();

            List<Symbol>? symbolsConfig = configuration.GetSection("Symbols").Get<List<Symbol>>();

            ServiceProvider serviceProvider = new ServiceCollection()
            .AddScoped<IInitialBalanceProvider, InitialBalanceProvider>(provider =>
            new InitialBalanceProvider(deposit))
            .AddScoped<ISymbolGeneratorService, SymbolGeneratorService>(provider =>
                new SymbolGeneratorService(symbolsConfig!))
            .AddScoped<ISlotMachineService, SlotMachineService>()
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}

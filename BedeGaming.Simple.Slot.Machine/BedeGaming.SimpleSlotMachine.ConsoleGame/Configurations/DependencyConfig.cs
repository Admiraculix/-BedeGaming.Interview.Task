using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using BedeGaming.SimpleSlotMachine.Application.Providers;
using BedeGaming.SimpleSlotMachine.Application.Services;
using BedeGaming.SimpleSlotMachine.Application.Validators;
using BedeGaming.SimpleSlotMachine.Domain;
using BedeGaming.SimpleSlotMachine.Domains;
using Consoles.Common;
using Consoles.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BedeGaming.SimpleSlotMachine.ConsoleGame.Configurations
{
    public static class DependencyConfig
    {
        public static ServiceProvider ConfigureDependencies()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                      .AddJsonFile(Path.Combine("Configurations", "appsettings.json"), optional: false, reloadOnChange: true)
                      .Build();

            List<Symbol>? symbolsConfig = configuration.GetSection("Symbols").Get<List<Symbol>>();
            Dimensions dimensions = configuration.GetSection("Dimensions").Get<Dimensions> (); //TODO need to be passed to slot mashine service

            ServiceProvider serviceProvider = new ServiceCollection()
            .AddScoped<IConsoleInputReader, ConsoleInputReader>()
            .AddScoped<IDepositValidator, DepositValidator>()
            .AddScoped<IStakeValidator, StakeValidator>()
            .AddScoped<IInitialBalanceProvider, InitialBalanceProvider>(provider =>
                new InitialBalanceProvider(
                provider.GetRequiredService<IDepositValidator>(),
                provider.GetRequiredService<IConsoleInputReader>()))
            .AddScoped<ISymbolGeneratorService, SymbolGeneratorService>(provider =>
                new SymbolGeneratorService(symbolsConfig!))
            .AddScoped<ISlotMachineService, SlotMachineService>(provider =>
                new SlotMachineService(
                    provider.GetRequiredService<IInitialBalanceProvider>(),
                    provider.GetRequiredService<ISymbolGeneratorService>(),
                    provider.GetRequiredService<IConsoleInputReader>(),
                    provider.GetRequiredService<IStakeValidator>())
                )
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}

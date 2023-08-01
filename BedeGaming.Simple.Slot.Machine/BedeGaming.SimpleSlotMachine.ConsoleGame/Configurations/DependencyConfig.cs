using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.Application.Providers;
using BedeGaming.SimpleSlotMachine.Application.Services;
using BedeGaming.SimpleSlotMachine.Application.Validators;
using BedeGaming.SimpleSlotMachine.Domain;
using Consoles.Common.Interfaces;
using Consoles.Common;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BedeGaming.SimpleSlotMachine.Domains;

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
            Dimensions dimensions = configuration.GetSection("Dimensions").Get<Dimensions> ();

            ServiceProvider serviceProvider = new ServiceCollection()
            .AddScoped<IConsoleInputReader, ConsoleInputReader>()
            .AddScoped<IValidator<double>, DepositValidator>()
            .AddScoped<IInitialBalanceProvider, InitialBalanceProvider>(provider =>
                new InitialBalanceProvider(
                provider.GetRequiredService<IValidator<double>>(),
                provider.GetRequiredService<IConsoleInputReader>()))
            .AddScoped<ISymbolGeneratorService, SymbolGeneratorService>(provider =>
                new SymbolGeneratorService(symbolsConfig!))
            .AddScoped<ISlotMachineService, SlotMachineService>()
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}

using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;
using BedeGaming.SimpleSlotMachine.ConsoleGame.Configurations;
using Consoles.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BedeGaming.SimpleSlotMachine.ConsoleGame
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Simplified Slot Machine!");

            ServiceProvider serviceProvider = DependencyConfig.ConfigureDependencies();
            IConsoleInputReader consoleInputReader = serviceProvider.GetRequiredService<IConsoleInputReader>();
            IInitialBalanceProvider initialBalanceProvider = serviceProvider.GetRequiredService<IInitialBalanceProvider>();

            // Read the initial deposit amount from the user
            initialBalanceProvider.Deposit = consoleInputReader.ReadValidInput<double>(Мessages.Balance.InitialDepositPrompt);

            // Read the stake amount from the user
            var stakeAmount = consoleInputReader.ReadValidInput<double>(Мessages.SlotMachine.StakeAmountPrompt);
            ISlotMachineService slotMachine = serviceProvider.GetRequiredService<ISlotMachineService>();

            // Start the slot machine game
            slotMachine.Play(stakeAmount);
        }
    }
}
using BedeGaming.SimpleSlotMachine.Application.Interfaces;
using BedeGaming.SimpleSlotMachine.ConsoleGame.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BedeGaming.SimpleSlotMachine.ConsoleGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Simplified Slot Machine!");

            Console.Write("Please enter your initial deposit amount: ");
            int deposit = int.Parse(Console.ReadLine());

            Console.Write("Please enter your stake amount: ");
            int stakeAmount = int.Parse(Console.ReadLine());

            var serviceProvider = DependencyConfig.ConfigureDependencies(deposit);

            var slotMachine = serviceProvider.GetRequiredService<ISlotMachineService>();
            slotMachine.Play(stakeAmount);
        }
    }
}
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Providers;

namespace BedeGaming.SimpleSlotMachine.Application.Providers
{
    public class InitialBalanceProvider : IInitialBalanceProvider
    {
        private int _deposit;

        public InitialBalanceProvider(int deposit)
        {
            _deposit = deposit;
        }

        public int GetInitialBalance()
        {
            return _deposit;
        }

        public int GetInitialBalanceConsole()
        {
            Console.Write("Please enter your initial deposit amount: ");
            return int.Parse(Console.ReadLine());
        }
    }
}

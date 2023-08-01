namespace BedeGaming.SimpleSlotMachine.Application.Constants
{
    public static class Мessages
    {
        public static class Balance
        {
            public static string InitialDepositPrompt => "Please enter your initial deposit amount: ";

            public static string DepositAmoutShouldBeGreaterThanZero => "Deposit amount must be greater than 0.";
        }

        public static class SlotMachine
        {
            public static string GameOver => "Game over! You have no balance left.Thank you for playing!";
            public static string SpinResults => "Spin Result:";
            public static string YouWin(double winAmount, double balance) => $"You won: {winAmount}, Current balance: {balance}";
            public static string StakeAmountPrompt => "Please enter your stake amount: ";
        }

        public static class SymbolGenerator
        {

        }
    }
}

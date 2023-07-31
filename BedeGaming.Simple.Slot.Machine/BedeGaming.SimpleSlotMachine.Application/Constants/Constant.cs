namespace BedeGaming.SimpleSlotMachine.Application.Constants
{
    public static class Constant
    {
        public static class Balance
        {
            public static string InitialDeposit => "Please enter your initial deposit amount: ";

            public static string DepositAmountValidation => "Deposit amount must be greater than 0.";
        }

        public static class SlotMachine
        {
            public static string GameOver => "\nGame over! You have no balance left.Thank you for playing!";
            public static string SpinResults => "\nSpin Result:";
            public static string YouWin(double winAmount, double balance) => $"You won: {winAmount}, Current balance: {balance}";
            public static string StakeAmount => "Please enter your stake amount: ";
        }

        public static class SymbolGenerator
        {

        }
    }
}

namespace BedeGaming.SimpleSlotMachine.Domain
{
    public class Symbol
    {
        public string Name { get; set; }
        public double Coefficient { get; set; }
        public int Probability { get; set; }

        public Symbol(string name, double coefficient, int probability)
        {
            Name = name;
            Coefficient = coefficient;
            Probability = probability;
        }
    }
}
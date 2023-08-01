namespace BedeGaming.SimpleSlotMachine.Application.Interfaces
{
    public interface ISlotMachineService
    {
        public double Balance { get;}

        void Play(double stakeAmount);
    }
}

namespace BedeGaming.SimpleSlotMachine.Application.Interfaces
{
    public interface ISlotMachineService
    {
        public decimal Balance { get;}
        void Play(decimal stakeAmount);
    }
}

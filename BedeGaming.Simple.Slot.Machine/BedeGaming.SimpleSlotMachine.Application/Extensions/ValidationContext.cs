using BedeGaming.SimpleSlotMachine.Application.Services;
using FluentValidation;

namespace BedeGaming.SimpleSlotMachine.Application.Extensions
{
    public static class ValidationContextExtensions
    {
        private const string SlotMachineServiceKey = "SlotMachineService";

        public static void SetSlotMachineService(this ValidationContext<double> context, SlotMachineService service)
        {
            context.RootContextData[SlotMachineServiceKey] = service;
        }

        public static SlotMachineService GetSlotMachineService(this ValidationContext<double> context)
        {
            return context.RootContextData.ContainsKey(SlotMachineServiceKey)
                ? context.RootContextData[SlotMachineServiceKey] as SlotMachineService
                : null;
        }
    }

}

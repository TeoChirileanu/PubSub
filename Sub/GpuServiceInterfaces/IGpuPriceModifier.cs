using System;
using Messages;

namespace Sub.GpuServiceInterfaces
{
    public interface IGpuPriceModifier
    {
        Gpu ModifyPrice(Gpu gpu)
        {
            Console.WriteLine($"Modifying Price for {gpu}");
            // todo: apply modify price strategy
            return gpu;
        }
    }
}
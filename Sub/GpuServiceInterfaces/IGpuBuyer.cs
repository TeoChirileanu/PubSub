using System;
using Messages;

namespace Sub.GpuServiceInterfaces
{
    public interface IGpuBuyer
    {
        void Buy(Gpu gpu) => Console.WriteLine($"Buying GPU: {gpu}");
    }
}
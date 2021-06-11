using System;
using Messages;

namespace Sub.GpuServiceInterfaces
{
    public interface IGpuPoster
    {
        void Post(Gpu gpu) => Console.WriteLine($"Posting GPU: {gpu}");
    }
}
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Messages;
using Sub.GpuServiceInterfaces;

namespace Sub
{
    public record Subscriber(string Name,
        IGpuBuyer Buyer, IGpuPriceModifier Modifier, IGpuReseller Reseller) // todo: make them readonly, maybe group
    {
        public IList<Gpu> ScalpedGpus { get; } = new List<Gpu>();

        private void OnNext(Gpu gpu)
        {
            // todo: add logging, persistence, monitoring

            Console.WriteLine($"[{Name}]: Buying GPU: {gpu}");
            Buyer.Buy(gpu);

            var updatedGpu = Modifier.ModifyPrice(gpu);
            Console.WriteLine($"[{Name}]: Updating GPU price from {gpu.Price} to {updatedGpu.Price}");

            Console.WriteLine($"[{Name}]: Reselling GPU: {gpu}");
            Reseller.Resell(updatedGpu);

            ScalpedGpus.Add(updatedGpu); // todo: transition to thread-safe collection
        }

        public void Subscribe(Subject<Gpu> topic)
        {
            topic.Subscribe(OnNext); // todo: async, multi-threaded
        }
    }
}
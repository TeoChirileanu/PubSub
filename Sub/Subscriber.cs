using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Messages;
using Sub.GpuServiceInterfaces;

namespace Sub
{
    public record Subscriber(IGpuBuyer Buyer, IGpuPriceModifier Modifier, IGpuPoster Poster) // todo: make them readonly
    {
        public IList<Gpu> PostedGpus { get; } = new List<Gpu>();

        private void OnNext(Gpu gpu)
        {
            Buyer.Buy(gpu);
            var updatedGpu = Modifier.ModifyPrice(gpu);
            Poster.Post(updatedGpu);
            PostedGpus.Add(gpu);
        }

        public void Subscribe(Subject<Gpu> topic) => topic.Subscribe(OnNext);
    }
}
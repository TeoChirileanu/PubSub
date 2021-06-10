using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Console = System.Console;

namespace ReactiveImpl.Test
{
    public record Subscriber(Subject<Gpu> GpuListing, IGpuBuyer Buyer, IGpuPriceModifier PriceModifier, IGpuPoster Poster)
    {
        public IList<Gpu> PostedGpus { get; } = new List<Gpu>();

        private void OnNext(Gpu gpu)
        {
            var boughtGpu = Buyer.Buy(gpu);
            var boughtGpuHavingModifiedPrice = PriceModifier.ModifyPrice(boughtGpu);
            var postedGpu = Poster.Post(boughtGpuHavingModifiedPrice);
            PostedGpus.Add(postedGpu);
        }

        public void Subscribe() => GpuListing.Subscribe(OnNext);
    }

    public interface IGpuBuyer
    {
        Gpu Buy(Gpu gpu)
        {
            Console.WriteLine($"Buying GPU: {gpu}");
            return gpu;
        }
    }

    public class GpuBuyer : IGpuBuyer {}

    public interface IGpuPriceModifier
    {
        Gpu ModifyPrice(Gpu gpu)
        {
            Console.WriteLine($"Changing price from {gpu.Price} to {gpu.Price * 2}");
            return new Gpu(gpu.Name, gpu.Price * 2);
        }
    }

    public class GpuPriceModifier : IGpuPriceModifier {}

    public interface IGpuPoster
    {
        Gpu Post(Gpu gpu)
        {
            Console.WriteLine($"Posting GPU: {gpu}");
            return gpu;
        }
    }

    public class GpuPoster : IGpuPoster {}
}
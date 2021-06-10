using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Console = System.Console;

namespace ReactiveImpl.Test
{
    public class Subscriber
    {
        private readonly IGpuBuyer _gpuBuyer;
        private readonly IGpuPriceModifier _gpuPriceModifier;
        private readonly IGpuPoster _gpuPoster;

        public IList<Gpu> PostedGpus { get; } = new List<Gpu>();

        public Subscriber(IGpuBuyer gpuBuyer, IGpuPriceModifier gpuPriceModifier, IGpuPoster gpuPoster)
        {
            _gpuBuyer = gpuBuyer;
            _gpuPriceModifier = gpuPriceModifier;
            _gpuPoster = gpuPoster;
        }

        public void Subscribe(Broker broker)
        {
            broker.GpuListing.ObserveOn(Scheduler.Default).Subscribe(OnNext);
        }

        private void OnNext(Gpu gpu)
        {
            var boughtGpu = _gpuBuyer?.Buy(gpu);
            var boughtGpuHavingModifiedPrice = _gpuPriceModifier?.ModifyPrice(boughtGpu);
            var postedGpu = _gpuPoster?.Post(boughtGpuHavingModifiedPrice);
            PostedGpus.Add(postedGpu);
        }
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
            return new Gpu(gpu.Price * 2);
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
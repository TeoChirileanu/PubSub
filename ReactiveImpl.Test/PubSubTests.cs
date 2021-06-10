using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using NFluent;
using NUnit.Framework;

namespace ReactiveImpl.Test
{
    public class PubSubTests
    {
        [Test]
        public void WhenAmdPostsNewGpu_ThenEmagShouldBuyItAndResellItAtDoubleItsPrice()
        {
            // Arrange
            Subject<Gpu> gpuListing = new();

            Publisher amd = new("AMD", gpuListing);

            Subscriber emag = new(gpuListing, new GpuBuyer(), new GpuPriceModifier(), new GpuPoster());
            emag.Subscribe();

            // Act
            Gpu rx550 = new("550", 80);
            amd.Publish(rx550);

            // Assert
            var latestGpu = emag.PostedGpus.Last();
            Check.That(latestGpu.Price).IsEqualTo(rx550.Price * 2);
        }

        [Test]
        public void ShouldWorkWith3SubscribersAnd2Publishers()
        {
            // Arrange
            Subject<Gpu> gpuListing = new();

            Publisher amd = new("AMD", gpuListing);
            Publisher nvidia= new("Nvidia", gpuListing);

            Subscriber emag = new(gpuListing, new GpuBuyer(), new GpuPriceModifier(), new GpuPoster());
            emag.Subscribe();
            Subscriber cel = new(gpuListing, new GpuBuyer(), new GpuPriceModifier(), new GpuPoster());
            cel.Subscribe();
            Subscriber forit = new(gpuListing, new GpuBuyer(), new GpuPriceModifier(), new GpuPoster());
            forit.Subscribe();

            // Act
            Gpu rx550 = new("550", 80);
            amd.Publish(rx550);
            Gpu rtx3070 = new("3070", 499);
            nvidia.Publish(rtx3070);

            // Assert
            var latestGpu = emag.PostedGpus.Last();
            Check.That(latestGpu.Price).IsEqualTo(rtx3070.Price * 2);
            var earliestGpu = cel.PostedGpus.First();
            Check.That(earliestGpu.Price).IsEqualTo(rx550.Price * 2);
            Check.That(forit.PostedGpus).ContainsExactly(new List<Gpu>
            {
                new(rx550.Name, rx550.Price * 2), new(rtx3070.Name, rtx3070.Price * 2),
            });
        }
    }
}
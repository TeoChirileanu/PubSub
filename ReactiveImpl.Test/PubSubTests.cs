using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace ReactiveImpl.Test
{
    public class PubSubTests
    {
        [Test]
        public void WhenAmdPostNewGpu_ThenEmagShouldBuyItAndResellItAtDoubleItsPrice()
        {
            // Arrange
            var gpuListing = new Broker();
            Publisher nvidia = new(gpuListing);

            Subscriber emag = new(new GpuBuyer(), new GpuPriceModifier(), new GpuPoster());
            emag.Subscribe(gpuListing);

            // Act
            Gpu rtx3070 = new(599);
            nvidia.Publish(rtx3070);

            Thread.Sleep(100);

            // Assert
            var latestGpu = emag.PostedGpus.Last();
            Check.That(latestGpu.Price).IsStrictlyGreaterThan(rtx3070.Price);
        }
    }

    public class Gpu
    {
        public Gpu(int price) => Price = price;

        public int Price { get; }
    }
}
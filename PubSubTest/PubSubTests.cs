using System.Linq;
using Messages;
using NFluent;
using NSubstitute;
using NUnit.Framework;
using Pub;
using Sub;
using Sub.GpuServiceInterfaces;
using static Broker.Topic;

namespace PubSubTest
{
    public class PubSubTests
    {
        private Gpu _gpu;

        private IGpuBuyer _gpuBuyer;

        // todo: unit-test each separate component
        private IGpuPriceModifier _gpuPriceModifier;
        private IGpuReseller _gpuReseller;

        [SetUp]
        public void SetUp()
        {
            _gpuPriceModifier = Substitute.For<IGpuPriceModifier>();
            _gpuBuyer = Substitute.For<IGpuBuyer>();
            _gpuReseller = Substitute.For<IGpuReseller>();
            _gpu = new Gpu(nameof(Gpu), 1337);

            _gpuPriceModifier.ModifyPrice(Arg.Any<Gpu>()).Returns(_gpu);
        }

        [Test]
        public void WhenAPublisherPublishesAGpu_ThenASubscriberShouldBeNotifiedAboutTheGpuAndScalpIt()
        {
            // Arrange
            var publisher = new Publisher(nameof(Publisher));
            publisher.PublishOn(GpuListing);

            var subscriber = new Subscriber(nameof(Subscriber), _gpuBuyer, _gpuPriceModifier, _gpuReseller);
            subscriber.Subscribe(GpuListing);

            // Act
            publisher.Publish(_gpu);

            // Assert
            Check.That(subscriber.ScalpedGpus).Not.IsNullOrEmpty();
            Check.That(subscriber.ScalpedGpus).CountIs(1);
            Check.That(subscriber.ScalpedGpus.Single()).IsEqualTo(_gpu);
        }
    }
}
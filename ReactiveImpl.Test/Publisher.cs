using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReactiveImpl.Test
{
    public class Publisher
    {
        private readonly Broker _broker;

        public Publisher(Broker broker) => _broker = broker;

        public void Publish(Gpu gpu) => _broker.Enqueue(gpu);
    }

    public class Broker
    {
        private readonly Subject<Gpu> _gpus = new();
        public IConnectableObservable<Gpu> GpuListing { get; }

        public Broker()
        {
            GpuListing = _gpus.ObserveOn(Scheduler.Default).Publish();
            GpuListing.Connect();
        }

        public void Enqueue(Gpu gpu) => _gpus.OnNext(gpu);
    }
}
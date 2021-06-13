using System.Reactive.Subjects;
using Messages;

namespace Broker
{
    public static class Topic // todo: avoid static classes (except extension methods)
    {
        // I found RX to be the most elegant solution to pub-sub
        public static Subject<Gpu> GpuListing { get; } = new();
    }
}
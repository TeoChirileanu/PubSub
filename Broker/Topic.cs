using System.Reactive.Subjects;
using Messages;

namespace Broker
{
    public static class Topic // I usually avoid static classes but I'll give this one a try this time
    {
        public static Subject<Gpu> GpuListing { get; } = new(); // make it static -> make it system-wide
    }
}
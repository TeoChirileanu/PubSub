using System.Reactive.Subjects;
using Messages;

namespace Broker
{
    public class Topic
    {
        public Subject<Gpu> Listing { get; } = new();
    }
}
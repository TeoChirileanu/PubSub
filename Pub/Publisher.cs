using System;
using System.Reactive.Subjects;
using Messages;

namespace Pub
{
    public record Publisher(string Name)
    {
        private Subject<Gpu> _topic;

        public void PublishOn(Subject<Gpu> topic)
        {
            _topic = topic;
        }

        public void Publish(Gpu gpu)
        {
            Console.WriteLine($"[{Name}]: Publishing {gpu}");
            _topic.OnNext(gpu);
        }
    }
}
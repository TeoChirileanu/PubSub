using System.Reactive.Subjects;
using Messages;

namespace Pub
{
    public record Publisher(string Name, Subject<Gpu> Topic)
    {
        public void Publish(Gpu gpu) => Topic.OnNext(gpu);
    }
}
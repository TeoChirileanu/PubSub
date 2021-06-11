using System.Reactive.Subjects;
using Messages;

namespace Pub
{
    public record Publisher(string Name, Subject<Gpu> GpuListing)
    {
        public void Publish(Gpu gpu)
        {
            GpuListing.OnNext(gpu);
        }
    }
}
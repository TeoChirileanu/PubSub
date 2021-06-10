using System.Reactive.Subjects;

namespace ReactiveImpl.Test
{
    public record Publisher(string Name, Subject<Gpu> GpuListing)
    {
        public void Publish(Gpu gpu) => GpuListing.OnNext(gpu);
    }

    public record Gpu(string Name, int Price);
}
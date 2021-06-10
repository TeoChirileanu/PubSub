using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReactiveImpl
{
    public interface IJob {}

    public class JobA : IJob {}

    public class JobB : IJob { }

    public class PubSub : IDisposable
    {
        private readonly Subject<IJob> _jobs = new();
        private readonly IConnectableObservable<IJob> _observable;

        public PubSub()
        {
            _observable = _jobs.ObserveOn(Scheduler.Default).Publish();
            _observable.Connect();
        }

        public void Enqueue(IJob job) => _jobs.OnNext(job);

        public void RegisterHandler<T>(Action<T> action) where T : IJob => _observable.OfType<T>().Subscribe(action);

        public void Dispose() => _jobs?.Dispose();
    }

    public static class Global
    {
        public static int Counter = 0;
    }
}
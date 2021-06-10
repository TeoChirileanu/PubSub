using System;
using System.Threading;
using System.Threading.Tasks;
using ReactiveImpl;

using var queue = new PubSub();

queue.RegisterHandler<JobA>(job => Console.WriteLine(Global.Counter));
queue.RegisterHandler<JobB>(job => Global.Counter++);

queue.Enqueue(new JobA());
queue.Enqueue(new JobB());
queue.Enqueue(new JobA());
queue.Enqueue(new JobB());
queue.Enqueue(new JobB());
queue.Enqueue(new JobA());

await Task.Delay(Timeout.InfiniteTimeSpan);
using System;
using System.Threading.Tasks;
using Messages;

namespace Pub
{
    public class Publisher
    {
        public string Name { get; }

        public Publisher(string name) => Name = name;

        public delegate void Notify(Publisher p, ItemAlert e);

        public event Notify OnPublish = null!;

        public async Task Publish()
        {
            // what if make it a background service?
            while (true)
            {
                Console.WriteLine("stock alert!");
                var duration = new Random().Next(1, 5);
                // what if waiting for some file with item info to appear?
                await Task.Delay(TimeSpan.FromSeconds(duration)); // simulate work
                OnPublish(this, new ItemAlert("New RTX 3070 available"));
                //Handler.Publish.Invoke(this, new ItemAlert("New RTX 3070 available")); // make a json with relevant info
            }
        }
    }
}
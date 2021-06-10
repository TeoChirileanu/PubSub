using System;
using Messages;
using Pub;

namespace Sub
{
    public class Subscriber
    {
        private readonly string _name;

        public Subscriber(string name) => _name = name;
         
        // todo: how to get rid of publisher ref?
        public void Subscribe(Publisher publisher) =>
            // what if make it reactive?
            publisher.OnPublish += NotificationReceived;

        // todo: unsubscribe

        private void NotificationReceived(object o, ItemAlert item) =>
            Console.WriteLine($"[{_name}]: Wow, a new GPU is in stock!! {item.ItemInfo}");
    }
}
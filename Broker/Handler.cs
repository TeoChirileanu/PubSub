using System;
using Messages;

namespace Broker
{
    public static class Handler
    {
        public static event EventHandler<ItemAlert> Publish;

        public static void OnPublish(ItemAlert e) => Publish?.Invoke(new object(), e);
    }
}

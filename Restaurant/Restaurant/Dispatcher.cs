using System;
using System.Collections.Generic;

namespace Restaurant
{
    public sealed class Dispatcher
    {
        public static readonly Dispatcher Instance = new Dispatcher();

        private readonly Dictionary<string, List<IOrderHandler>> subscriptions = new Dictionary<string, List<IOrderHandler>>();

        private readonly object lockObject = new object();
        public void Subscribe(string topic, IOrderHandler handler)
        {
            lock (lockObject)
            {
                List<IOrderHandler> handlers = subscriptions.ContainsKey(topic)
                                    ? new List<IOrderHandler>(subscriptions[topic])
                                    : new List<IOrderHandler>();
                handlers.Add(handler);
                subscriptions[topic] = handlers;
            }
        }

        public void Publish(string topic, Order order)
        {
            if (subscriptions.ContainsKey(topic))
            {
                foreach (var handler in subscriptions[topic])
                {
                    handler.Handle(order);
                }
            }
            else
            {
                Console.WriteLine("No subscribers found for topic " + topic);
            }
        }
    }
}

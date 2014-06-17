using System;
using System.Collections.Generic;

namespace Restaurant
{
    using Restaurant.Events;

    public sealed class Dispatcher
    {
        public static readonly Dispatcher Instance = new Dispatcher();

        private readonly Dictionary<Type, List<IEventHandler<IEvent>>> subscriptions =
            new Dictionary<Type, List<IEventHandler<IEvent>>>();
        private readonly Dictionary<Guid, List<IEventHandler<IEvent>>> subscriptionsByCorrelation =
            new Dictionary<Guid, List<IEventHandler<IEvent>>>();

        private readonly object lockObject = new object();
        public void Subscribe<T>(Type type, IEventHandler<T> handler) where T : IEvent
        {
            lock (lockObject)
            {
                List<IEventHandler<IEvent>> handlers = subscriptions.ContainsKey(type)
                                    ? new List<IEventHandler<IEvent>>(subscriptions[type])
                                    : new List<IEventHandler<IEvent>>();
                handlers.Add((IEventHandler<IEvent>)handler);
                subscriptions[type] = handlers;
            }
        }

        public void Subscribe<T>(Guid correlationId, IEventHandler<T> handler) where T : IEvent
        {
            lock (lockObject)
            {
                List<IEventHandler<IEvent>> handlers = subscriptionsByCorrelation.ContainsKey(correlationId)
                                    ? new List<IEventHandler<IEvent>>(subscriptionsByCorrelation[correlationId])
                                    : new List<IEventHandler<IEvent>>();
                handlers.Add((IEventHandler<IEvent>)handler);
                subscriptionsByCorrelation[correlationId] = handlers;
            }
        }

        public void Publish(IEvent @event)
        {
            if (subscriptions.ContainsKey(@event.GetType()))
            {
                foreach (var handler in subscriptions[@event.GetType()])
                {
                    handler.Handle(@event);
                }
            }

            if (subscriptionsByCorrelation.ContainsKey(@event.CorrelationId))
            {
                foreach (var handler in subscriptionsByCorrelation[@event.CorrelationId])
                {
                    handler.Handle(@event);
                }
            }

        }
    }
}

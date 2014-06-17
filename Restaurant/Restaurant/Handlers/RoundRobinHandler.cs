using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    using Restaurant.Events;

    /// <summary>
    /// Pass work in a round robin fashion
    /// </summary>
    public class RoundRobinHandler<T> : IEventHandler<T>
        where T : IEvent
    {
        private readonly List<IEventHandler<T>> handlers;

        public RoundRobinHandler(IEnumerable<IEventHandler<T>> handlers)
        {
            this.handlers = handlers.ToList();
        }

        public void Handle(T @event)
        {
            IEventHandler<T> handler = handlers[0];
            handler.Handle(@event);
            handlers.Remove(handler);
            handlers.Add(handler);
        }

        public string Name
        {
            get
            {
                return String.Empty;
            }
        }

    }
}

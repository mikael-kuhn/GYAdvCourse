using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Restaurant
{
    using Restaurant.Events;
    using Restaurant.Handlers;

    /// <summary>
    /// Distributes the work according to how much work the actor (handler) already has, ie. the message queue in front of the 
    /// actor
    /// </summary>
    public class BetterDispatcher : IEventHandler<IEvent>
    {
        private readonly List<QueuedHandler> nextHandlers;

        public BetterDispatcher(IEnumerable<QueuedHandler> nextHandlers)
        {
            this.nextHandlers = nextHandlers.ToList();
        }

        public void Handle(IEvent @event)
        {
            bool dispatched = false;
            while (!dispatched)
            {
                var nextHandler = nextHandlers.Where(x => x.MessageCount < 5).OrderBy(x => x.MessageCount).FirstOrDefault();
                if (nextHandler == null)
                {
                    Thread.Sleep(50);
                }
                else
                {
                    nextHandler.Handle(@event);
                    dispatched = true;
                }
            }
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

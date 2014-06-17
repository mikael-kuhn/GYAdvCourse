using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    /// <summary>
    /// Pass work in a round robin fashion
    /// </summary>
    public class RoundRobinHandler : IOrderHandler
    {
        private readonly List<IOrderHandler> handlers;


        public RoundRobinHandler(IEnumerable<IOrderHandler> handlers)
        {
            this.handlers = handlers.ToList();
        }

        public void Handle(Order order)
        {
            IOrderHandler handler = handlers[0];
            handler.Handle(order);
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

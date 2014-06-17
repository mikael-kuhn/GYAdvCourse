using System;
using System.Collections.Generic;

namespace Restaurant
{
    /*
    /// <summary>
    /// Pass the same order to all the next actors
    /// </summary>
    public class Multiplexor : IOrderHandler
    {
        private readonly IEnumerable<IOrderHandler> nextHandlers;

        public Multiplexor(IEnumerable<IOrderHandler> nextHandlers)
        {
            this.nextHandlers = nextHandlers;
        }

        public void Handle(Order order)
        {
            foreach (var handler in nextHandlers)
            {
                handler.Handle(order);
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
     * */
}
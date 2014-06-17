
namespace Restaurant
{
    using System;
    using System.Collections.Generic;

    public class Multiplexor : IOrderHandler
    {
        private readonly IEnumerable<IOrderHandler> _handlers;

        public Multiplexor(IEnumerable<IOrderHandler> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(Order order)
        {
            foreach (var handler in _handlers)
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
}
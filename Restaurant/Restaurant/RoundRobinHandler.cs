using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class RoundRobinHandler : IOrderHandler
    {
        private readonly List<IOrderHandler> _handlers;


        public RoundRobinHandler(IEnumerable<IOrderHandler> handlers)
        {
            _handlers = handlers.ToList();
        }

        public void Handle(Order order)
        {
            IOrderHandler handler = _handlers[0];
            handler.Handle(order);
            _handlers.Remove(handler);
            _handlers.Add(handler);
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

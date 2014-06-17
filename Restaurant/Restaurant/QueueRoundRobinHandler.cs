using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    using System.Threading;

    public class BetterDispatcher : IOrderHandler
    {
        private readonly List<QueuedHandler> handlers;
        public BetterDispatcher(IEnumerable<QueuedHandler> handlers)
        {
            this.handlers = handlers.ToList();
        }

        public void Handle(Order order)
        {
            bool dispatched = false;
            while (!dispatched)
            {
                var handler = handlers.Where(x => x.MessageCount < 5).OrderBy(x => x.MessageCount).FirstOrDefault();
                if (handler == null)
                {
                    Thread.Sleep(50);
                }
                else
                {
                    handler.Handle(order);
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

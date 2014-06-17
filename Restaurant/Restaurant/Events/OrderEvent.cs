using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Events
{
    public abstract class OrderEvent : IEvent
    {
        protected OrderEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; private set; }

        public DateTime? TimeToLive { get; set; }
    }
}

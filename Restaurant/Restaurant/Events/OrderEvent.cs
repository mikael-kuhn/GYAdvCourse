using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Events
{
    public abstract class OrderEvent : IEvent
    {
        protected OrderEvent(Order order, Guid causationId)
        {
            CausationId = causationId;
            CorrelationId = new Guid(order.Id);
            Id = Guid.NewGuid();
            Order = order;
        }

        public Order Order { get; private set; }

        public DateTime? TimeToLive { get; set; }

        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid CausationId { get; set; }

    }
}

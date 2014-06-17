namespace Restaurant.Actors
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Restaurant.Events;

    public class Cashier : IEventHandler<IEvent>
    {
        private readonly ConcurrentDictionary<string, Order> outstandingOrders;

        public Cashier()
        {
            outstandingOrders = new ConcurrentDictionary<string, Order>();
        }

        public void Handle(IEvent @event)
        {
            var orderPricedEvent = (OrderPriced)@event;
            Order order = orderPricedEvent.Order;
            outstandingOrders.AddOrUpdate(order.Id, order, (key, o) => o);
        }

        public void Pay(string id, string card)
        {
            Order order;
            outstandingOrders.TryRemove(id, out order);
            order.Card = card;
            Dispatcher.Instance.Publish(new PaymentTaken(order));
        }

        public IEnumerable<string> GetOutstandingOrders()
        {
            return outstandingOrders.Values.Select(o => o.Id);
        }

        public string Name
        {
            get
            {
                return "Cashier";
            }
        }
    }
}
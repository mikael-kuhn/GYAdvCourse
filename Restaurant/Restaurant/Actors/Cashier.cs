namespace Restaurant.Actors
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Restaurant.Commands;
    using Restaurant.Events;

    public class Cashier : IEventHandler<IEvent>
    {
        private readonly ConcurrentDictionary<string, Tuple<Order, Guid>> outstandingOrders;

        public Cashier()
        {
            outstandingOrders = new ConcurrentDictionary<string, Tuple<Order, Guid>>();
        }

        public void Handle(IEvent @event)
        {
            var orderPricedEvent = (TakePayment)@event;
            Order order = orderPricedEvent.Order;
            outstandingOrders.AddOrUpdate(order.Id, new Tuple<Order, Guid>(order, @event.Id), (key, o) => o);
            Pay(order.Id, "123123");
        }

        public void Pay(string id, string card)
        {
            Tuple<Order, Guid> orderInfo;
            outstandingOrders.TryRemove(id, out orderInfo);
            orderInfo.Item1.Card = card;
            Dispatcher.Instance.Publish(new PaymentTaken(orderInfo.Item1, orderInfo.Item2));
        }

        public IEnumerable<string> GetOutstandingOrders()
        {
            return outstandingOrders.Values.Select(o => o.Item1.Id);
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
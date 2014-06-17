namespace Restaurant.Actors
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class Cashier : IOrderHandler
    {
        private readonly IOrderHandler next;

        private readonly ConcurrentDictionary<string, Order> outstandingOrders;

        public Cashier(IOrderHandler next)
        {
            this.next = next;
            outstandingOrders = new ConcurrentDictionary<string, Order>();
        }

        public void Handle(Order order)
        {
            outstandingOrders.AddOrUpdate(order.Id, order, (key, o) => o);
        }

        public void Pay(string id, string card)
        {
            Order order;
            outstandingOrders.TryRemove(id, out order);
            order.Card = card;
            next.Handle(order);
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
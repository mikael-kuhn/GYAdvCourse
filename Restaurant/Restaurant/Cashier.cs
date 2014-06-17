using System.Linq;

namespace Restaurant
{
    using System.Collections.Generic;

    public class Cashier : IOrderHandler
    {
        private readonly IOrderHandler next;

        private readonly List<Order> outstandingOrders;

        public Cashier(IOrderHandler next)
        {
            this.next = next;
            outstandingOrders = new List<Order>();
        }

        public void Handle(Order order)
        {
            outstandingOrders.Add(order);
        }

        public void Pay(string id, string card)
        {
            Order order = outstandingOrders.First(o => o.Id == id);
            order.Card = card;
            next.Handle(order);
        }

        public IEnumerable<string> GetOutstandingOrders()
        {
            return outstandingOrders.Select(o => o.Id);
        }
    }
}
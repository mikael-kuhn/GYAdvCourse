using System.Collections.Generic;

namespace Restaurant
{
    public class Waiter
    {
        private readonly IOrderHandler orderHandler;

        private readonly string waiterName;

        private readonly Dictionary<int, Line> menu = new Dictionary<int, Line> { 
        { 1, new Line("razor blades special", "razor blades, hamburger", 2.99) },
        { 2, new Line("icecream", "vanilla icecream", 1.00) }};

        public Waiter(IOrderHandler orderHandler, string waiterName)
        {
            this.orderHandler = orderHandler;
            this.waiterName = waiterName;
        }

        public void PlaceOrder(IEnumerable<int> orderItems)
        {
            Order order = new Order(string.Empty) { Table = 2, Waiter = waiterName };
            foreach (int orderItem in orderItems)
            {
                order.AddLine(menu[orderItem]);
            }
            orderHandler.Handle(order);
        }
    }
}

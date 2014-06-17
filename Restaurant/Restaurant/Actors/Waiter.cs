namespace Restaurant.Actors
{
    using System.Collections.Generic;

    using Restaurant.Events;

    public class Waiter
    {
        private readonly string waiterName;

        private readonly Dictionary<int, Line> menu = new Dictionary<int, Line> { 
        { 1, new Line("razor blades special", "razor blades, hamburger", 2.99) },
        { 2, new Line("icecream", "vanilla icecream", 1.00) }};

        public Waiter(string waiterName)
        {
            this.waiterName = waiterName;
        }

        public void PlaceOrder(IEnumerable<int> orderItems)
        {
            Order order = new Order(string.Empty) { Table = 2, Waiter = waiterName };
            foreach (int orderItem in orderItems)
            {
                order.AddLine(menu[orderItem]);
            }
            Dispatcher.Instance.Publish(new OrderPlaced(order));
        }
    }
}

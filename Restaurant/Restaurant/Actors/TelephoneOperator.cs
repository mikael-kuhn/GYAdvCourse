namespace Restaurant.Actors
{
    using System;
    using System.Collections.Generic;

    using Restaurant.Events;

    public class TelephoneOperator
    {
        private readonly string operatorName;

        private readonly Dictionary<int, Line> menu = new Dictionary<int, Line> { 
        { 1, new Line("razor blades special", "razor blades, hamburger", 2.99) },
        { 2, new Line("icecream", "vanilla icecream", 1.00) },
        { 3, new Line("salad sandwich", "bread, butter, salad", 3.99)}};

        public TelephoneOperator(string operatorName)
        {
            this.operatorName = operatorName;
        }

        public Guid PlaceOrder(IEnumerable<int> orderItems)
        {
            Order order = new Order(string.Empty) { Table = 2, Waiter = operatorName };
            foreach (int orderItem in orderItems)
            {
                order.AddLine(menu[orderItem]);
            }
            order.IsDodgy = true;
            Dispatcher.Instance.Publish(new OrderPlaced(order));
            return new Guid(order.Id);
        }
    }
}

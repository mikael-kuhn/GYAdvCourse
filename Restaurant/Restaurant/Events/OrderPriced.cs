namespace Restaurant.Events
{
    public sealed class OrderPriced : OrderEvent
    {
        public OrderPriced(Order order)
            : base(order)
        {
        }
    }
}

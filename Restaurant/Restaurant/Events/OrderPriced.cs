namespace Restaurant.Events
{
    using System;

    public sealed class OrderPriced : OrderEvent
    {
        public OrderPriced(Order order, Guid causationId)
            : base(order, causationId)
        {
        }
    }
}

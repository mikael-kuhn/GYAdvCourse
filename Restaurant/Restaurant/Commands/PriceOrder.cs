namespace Restaurant.Commands
{
    using System;

    using Restaurant.Events;

    public class PriceOrder : OrderEvent
    {
        public PriceOrder(Order order, Guid causationId)
            : base(order, causationId)
        {
        }

    }
}
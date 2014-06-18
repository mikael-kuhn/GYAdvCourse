namespace Restaurant.Commands
{
    using System;

    using Restaurant.Events;

    public class SecondCookRetry : OrderEvent
    {
        public SecondCookRetry(Order order, Guid causationId)
            : base(order, causationId)
        {
        }
    }
}
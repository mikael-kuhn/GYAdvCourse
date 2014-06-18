namespace Restaurant.Commands
{
    using System;

    using Restaurant.Events;

    public class FirstCookRetry : OrderEvent
    {
        public FirstCookRetry(Order order, Guid causationId)
            : base(order, causationId)
        {
        }
    }
}
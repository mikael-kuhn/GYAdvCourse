namespace Restaurant.Commands
{
    using System;

    using Restaurant.Events;

    public class PrintOrder : OrderEvent
    {
        public PrintOrder(Order order, Guid causationId)
            : base(order, causationId)
        {
        }
    }
}
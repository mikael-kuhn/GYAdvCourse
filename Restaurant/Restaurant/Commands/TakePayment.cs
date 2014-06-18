namespace Restaurant.Commands
{
    using System;

    using Restaurant.Events;

    public class TakePayment : OrderEvent
    {
        public TakePayment(Order order, Guid causationId)
            : base(order, causationId)
        {
        }
    }
}
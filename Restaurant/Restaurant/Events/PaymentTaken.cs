namespace Restaurant.Events
{
    using System;

    public sealed class PaymentTaken : OrderEvent
    {
        public PaymentTaken(Order order, Guid causationId)
            : base(order, causationId)
        {
        }

    }
}

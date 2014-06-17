namespace Restaurant.Events
{
    using System;

    public sealed class FoodCooked : OrderEvent
    {
        public FoodCooked(Order order, Guid causationId)
            : base(order, causationId)
        {
        }

    }
}

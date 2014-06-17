
namespace Restaurant.Events
{
    using System;

    public sealed class OrderPlaced : OrderEvent
    {
        public OrderPlaced(Order order): base(order)
        {
            TimeToLive = DateTime.Now.Add(TimeSpan.FromSeconds(5));
        }

    }
}

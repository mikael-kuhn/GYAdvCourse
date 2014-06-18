namespace Restaurant.Commands
{
    using System;
    using Restaurant.Events;

    public class CookFood : OrderEvent
    {
        public CookFood(Order order, Guid causationId)
            : base(order, causationId)
        {
            TimeToLive = DateTime.Now.Add(TimeSpan.FromSeconds(5));
        }
    }
}
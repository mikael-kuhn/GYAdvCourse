namespace Restaurant.Events
{
    public sealed class FoodCooked : OrderEvent
    {
        public FoodCooked(Order order) : base(order)
        {
        }

    }
}

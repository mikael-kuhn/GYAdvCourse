namespace Restaurant.Events
{
    public sealed class PaymentTaken : OrderEvent
    {
        public PaymentTaken(Order order)
            : base(order)
        {
        }

    }
}

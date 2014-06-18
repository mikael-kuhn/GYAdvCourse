namespace Restaurant
{
    using System;

    using Restaurant.Commands;
    using Restaurant.Events;

    public class OrderMidget : IMidget
    {
        private readonly IDispatcher dispatcher;

        public Guid CorrelationId { get; private set; }

        public OrderMidget(Guid correlationId, IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            CorrelationId = correlationId;
        }

        public void Handle(OrderPlaced @event)
        {
            dispatcher.Publish(new CookFood(@event.Order, @event.Id));
        }

        public void Handle(FoodCooked @event)
        {
            dispatcher.Publish(new PriceOrder(@event.Order, @event.Id));
        }

        public void Handle(OrderPriced @event)
        {
            dispatcher.Publish(new TakePayment(@event.Order, @event.Id));
        }

        public void Handle(PaymentTaken @event)
        {
            dispatcher.Publish(new PrintOrder(@event.Order, @event.Id));
            if (OnCompleted != null)
            {
                OnCompleted(this, new EventArgs());
            }
        }

        public event EventHandler<EventArgs> OnCompleted;
    }
}
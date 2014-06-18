namespace Restaurant
{
    using System;

    using Restaurant.Actors;
    using Restaurant.Commands;
    using Restaurant.Events;

    public class OrderMidget : IMidget
    {
        private readonly IDispatcher dispatcher;
        private bool foodCooked;
        public Guid CorrelationId { get; private set; }

        public OrderMidget(Guid correlationId, IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            CorrelationId = correlationId;
            foodCooked = false;
        }

        public void Handle(OrderPlaced @event)
        {
            dispatcher.Publish(new CookFood(@event.Order, @event.Id));
            dispatcher.Publish(
                new SendToMeIn(CorrelationId, DateTime.Now.AddSeconds(6), new FirstCookRetry(@event.Order, @event.Id)));
        }

        public void Handle(FoodCooked @event)
        {
            foodCooked = true;
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

        public void Handle(FirstCookRetry @event)
        {
            if (foodCooked) return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Food not cooked yet, retrying, order {0}", CorrelationId);
            Console.ResetColor();
            dispatcher.Publish(new SecondCookRetry(@event.Order, @event.Id));
        }

        public void Handle(SecondCookRetry @event)
        {
            if (foodCooked) return;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Food still not cooked, seems like we never get it, order {0}", CorrelationId);
            Console.ResetColor();

            if (OnCompleted != null)
            {
                OnCompleted(this, new EventArgs());
            }
        }

        public event EventHandler<EventArgs> OnCompleted;
    }
}
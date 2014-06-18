namespace Restaurant.Handlers
{
    using System;

    using Restaurant.Events;

    public class PrintOrderHandler : IEventHandler<IEvent>
    {
        public void Handle(IEvent @event)
        {
            if (@event is OrderPlaced)
            {
                Dispatcher.Instance.Subscribe(@event.CorrelationId, this);
            }

            if (((OrderEvent)@event).Order.IsDodgy)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("DODGY ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Order completed: {0}, corr id {1}", @event.GetType().Name, @event.CorrelationId);
            Console.ResetColor();
        }
        public string Name
        {
            get
            {
                return String.Empty;
            }
        }

    }
}

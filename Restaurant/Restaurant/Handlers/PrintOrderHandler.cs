namespace Restaurant.Handlers
{
    using System;

    using Restaurant.Events;

    public class PrintOrderHandler : IEventHandler<IEvent>
    {
        public void Handle(IEvent @event)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Order finished: " + ((OrderEvent)@event).Order.Id);
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

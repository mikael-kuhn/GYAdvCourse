namespace Restaurant.Actors
{
    using System;
    using System.Threading;

    using Restaurant.Events;

    public class Cook : IEventHandler<IEvent>
    {
        private static readonly Random Random = new Random();
        private readonly string name;
        private readonly int cookTime;

        public Cook(string name)
        {
            cookTime = Random.Next(500);
            this.name = name;
        }

        public void Handle(IEvent @event)
        {
            Thread.Sleep(cookTime);

            var orderPlacedEvent = (OrderPlaced)@event;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Order cooked " + orderPlacedEvent.Order.Id);
            Console.ResetColor();

            orderPlacedEvent.Order.CookTime = cookTime;
            orderPlacedEvent.Order.Cook = name;
            Dispatcher.Instance.Publish(new FoodCooked(orderPlacedEvent.Order));
        }

        public string Name
        {
            get { return name; }
        }
    }
}

namespace Restaurant.Actors
{
    using System.Linq;
    using System.Threading;

    using Restaurant.Events;

    public class AssistingManager : IEventHandler<IEvent>
    {
        private readonly string name;

        public AssistingManager(string name)
        {
            this.name = name;
        }

        public void Handle(IEvent @event)
        {
            Thread.Sleep(100);
            Order order = ((FoodCooked)@event).Order;
            order.SubTotal = order.Lines.Sum(l => l.Price);
            order.Tax = order.SubTotal * 0.2;
            order.Total = order.SubTotal + order.Tax;
            Dispatcher.Instance.Publish(new OrderPriced(order));
        }

        public string Name {
            get
            {
                return name;
            }
        }
    }

}

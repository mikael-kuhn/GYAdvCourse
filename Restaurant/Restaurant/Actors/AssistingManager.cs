namespace Restaurant.Actors
{
    using System.Linq;
    using System.Threading;

    public class AssistingManager : IOrderHandler
    {
        private readonly string name;

        public AssistingManager(string name)
        {
            this.name = name;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(100);
            order.SubTotal = order.Lines.Sum(l => l.Price);
            order.Tax = order.SubTotal * 0.2;
            order.Total = order.SubTotal + order.Tax;
            Dispatcher.Instance.Publish("takepayment", order);
        }

        public string Name {
            get
            {
                return name;
            }
        }
    }

}

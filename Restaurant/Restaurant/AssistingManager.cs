using System.Linq;
using System.Threading;

namespace Restaurant
{
    public class AssistingManager : IOrderHandler
    {
        private readonly IOrderHandler next;

        private string name;

        public AssistingManager(IOrderHandler next, string name)
        {
            this.name = name;
            this.next = next;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(100);
            order.SubTotal = order.Lines.Sum(l => l.Price);
            order.Tax = order.SubTotal * 0.2;
            order.Total = order.SubTotal + order.Tax;
            next.Handle(order);
        }

        public string Name {
            get
            {
                return "Assisting Manager";
            }
        }
    }

}

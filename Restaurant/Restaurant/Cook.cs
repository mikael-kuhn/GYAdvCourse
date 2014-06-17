using System;
using System.Threading;

namespace Restaurant
{
    public class Cook : IOrderHandler
    {
        private readonly IOrderHandler next;
        private static readonly Random Random = new Random();
        private readonly string name;
        private readonly int cookTime;

        public Cook(IOrderHandler next, string name)
        {
            cookTime = Random.Next(500);
            this.name = name;
            this.next = next;
        }

        public void Handle(Order order)
        {
            
            Thread.Sleep(cookTime);
            order.CookTime = cookTime;
            order.Cook = name;
            next.Handle(order);
        }


        public string Name
        {
            get { return name; }
        }
    }
}

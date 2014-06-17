using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    using System.Threading;

    public class Cook : IOrderHandler
    {
        private readonly IOrderHandler next;

        private static Random random = new Random();
        private string name;
        private int cookTime;
        public Cook(IOrderHandler next, string name)
        {
            cookTime = random.Next(500);
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

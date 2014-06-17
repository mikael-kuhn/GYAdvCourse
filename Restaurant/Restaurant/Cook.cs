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

        private string name;

        public Cook(IOrderHandler next, string name)
        {
            this.name = name;
            this.next = next;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(500);
            order.CookTime = 5000;
            next.Handle(order);
        }
    }
}

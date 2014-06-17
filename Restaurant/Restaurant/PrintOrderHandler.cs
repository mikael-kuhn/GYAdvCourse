using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class PrintOrderHandler : IOrderHandler
    {
        public void Handle(Order order)
        {
            Console.WriteLine(order.ToString());
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

using System;

namespace Restaurant
{
    public class PrintOrderHandler : IOrderHandler
    {
        public void Handle(Order order)
        {
            //Console.WriteLine(order.ToString());
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

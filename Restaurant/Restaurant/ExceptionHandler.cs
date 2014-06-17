using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    using System.Globalization;
    using System.Threading;

    public sealed class ExceptionHandler : IOrderHandler
    {
        private readonly IOrderHandler next;

        public ExceptionHandler(IOrderHandler next)
        {
            this.next = next;
        }

        public void Handle(Order order)
        {
            int count = 3;
            bool handled = false;
            while (!handled)
            {
                try
                {
                    next.Handle(order);
                    handled = true;
                }
                catch (Exception e)
                {
                    if (count == 0)
                    {
                        break;
                    }
                    Thread.Sleep((3 - count--) * 10);
                }
            }
            if (!handled)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Dropped order id {0}", order.Id));
                Console.ResetColor();
            }
        }

        public string Name {
            get
            {
                return string.Empty;
            }
        }
    }
}

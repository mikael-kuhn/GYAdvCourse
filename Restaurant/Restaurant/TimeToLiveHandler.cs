using System;

namespace Restaurant
{
    using System.Globalization;

    public sealed class TimeToLiveHandler : IOrderHandler
    {
        private readonly IOrderHandler next;

        public TimeToLiveHandler(IOrderHandler next)
        {
            this.next = next;
        }

        public void Handle(Order order)
        {
            var now = DateTime.Now;
            if (order.TimeToLive < now)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Dropped order id {0}", order.Id));
                Console.ResetColor();
            }
            else
            {
                next.Handle(order);
            }
        }

        public string Name 
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

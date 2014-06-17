namespace Restaurant.Handlers
{
    using System;
    using System.Globalization;
    using System.Threading;

    using Restaurant.Events;

    public sealed class ExceptionHandler : IEventHandler<IEvent>
    {
        private readonly IEventHandler<IEvent> next;

        public ExceptionHandler(IEventHandler<IEvent> next)
        {
            this.next = next;
        }

        public void Handle(IEvent @event)
        {
            int count = 3;
            bool handled = false;
            while (!handled)
            {
                try
                {
                    next.Handle(@event);
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
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Dropped order id {0}", ((OrderEvent)@event).Order.Id));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Events;

namespace Restaurant.Handlers
{
    public sealed class Dropper : IEventHandler<IEvent>
    {
        private readonly IEventHandler<IEvent> next;

        private static Random random = new Random(100);

        public Dropper(IEventHandler<IEvent> next)
        {
            this.next = next;
        }

        public void Handle(IEvent @event)
        {
            int threshold = random.Next(100);
            if (threshold < 95)
            {
                next.Handle(@event);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Dropped Event: {0}, Id: {1}", @event.GetType().Name, @event.CorrelationId);
                Console.ResetColor();
            }
        }

        public
            string Name
        {
            get { return string.Empty; }
        }
    }
}

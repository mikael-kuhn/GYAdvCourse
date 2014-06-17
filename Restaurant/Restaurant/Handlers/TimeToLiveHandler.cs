namespace Restaurant.Handlers
{
    using System;

    using Restaurant.Events;

    public sealed class TimeToLiveHandler : IEventHandler<IEvent>
    {
        private readonly IEventHandler<IEvent> next;

        public TimeToLiveHandler(IEventHandler<IEvent> next)
        {
            this.next = next;
        }

        public void Handle(IEvent @event)
        {
            if (@event.TimeToLive != null)
            {
                var now = DateTime.Now;
                if (@event.TimeToLive < now)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Dropped order");
                    Console.ResetColor();
                }
                else
                {
                    next.Handle(@event);
                }
            }
            else
            {
                next.Handle(@event);
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

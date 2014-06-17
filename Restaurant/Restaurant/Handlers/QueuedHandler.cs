namespace Restaurant.Handlers
{
    using System.Collections.Concurrent;
    using System.Threading;

    using Restaurant.Events;

    /// <summary>
    /// Put a queue in front of the actor (handler)
    /// </summary>
    public class QueuedHandler : IEventHandler<IEvent>, IStartable
    {
        private readonly IEventHandler<IEvent> handler;
        private readonly ConcurrentQueue<IEvent> queue = new ConcurrentQueue<IEvent>();

        private void GetMessage()
        {
            while (true)
            {
                IEvent @event;
                if (queue.TryDequeue(out @event))
                {
                    //Console.WriteLine("Got an order to handle by " + handler.GetType().Name);
                    handler.Handle(@event);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        public QueuedHandler(IEventHandler<IEvent> handler)
        {
            this.handler = handler;
        }

        public void Start()
        {
            Thread thread = new Thread(GetMessage);
            thread.Start();
        }

        public void Handle(IEvent @event)
        {
            queue.Enqueue(@event);
        }

        public string Name {
            get
            {
                return handler.GetType().Name + " " + handler.Name;
            }
        }

        public int MessageCount
        {
            get
            {
                return queue.Count;
            }
        }
    }
}

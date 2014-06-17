using System.Threading;
using System.Collections.Concurrent;

namespace Restaurant
{
    /// <summary>
    /// Put a queue in front of the actor (handler)
    /// </summary>
    public class QueuedHandler : IOrderHandler, IStartable
    {
        private readonly IOrderHandler handler;
        private readonly ConcurrentQueue<Order> queue = new ConcurrentQueue<Order>();

        private void GetMessage()
        {
            while (true)
            {
                Order order;
                if (queue.TryDequeue(out order))
                {
                    //Console.WriteLine("Got an order to handle by " + handler.GetType().Name);
                    handler.Handle(order);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        public QueuedHandler(IOrderHandler handler)
        {
            this.handler = handler;
        }

        public void Start()
        {
            Thread thread = new Thread(GetMessage);
            thread.Start();
        }

        public void Handle(Order order)
        {
            queue.Enqueue(order);
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

using System;
using System.Collections.Generic;

namespace Restaurant
{
    using System.Globalization;
    using System.Threading;

    public class Monitor : IStartable
    {
        private readonly IEnumerable<QueuedHandler> handlers;

        public Monitor(IEnumerable<QueuedHandler> handlers)
        {
            this.handlers = handlers;
        }

        public void Start()
        {
            Thread thread = new Thread(Work);
            thread.Start();

        }

        public void Work()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                foreach (QueuedHandler handler in handlers)
                {
                    string name = handler.Name;
                    int count = handler.MessageCount;
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}, Count: {1}", name, count));
                }
                Console.ResetColor();
                Thread.Sleep(1000);
            }
        }
    }
}

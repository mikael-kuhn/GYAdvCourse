using System;
using System.Collections.Generic;

namespace Restaurant
{
    using System.Collections;
    using System.Globalization;
    using System.Threading;

    using Restaurant.Handlers;

    public class Monitor : IStartable
    {
        private readonly IEnumerable<QueuedHandler> handlers;

        private readonly IEnumerable<IMidgetHouse> houses;

        public Monitor(IEnumerable<QueuedHandler> handlers, IEnumerable<IMidgetHouse> houses)
        {
            this.handlers = handlers;
            this.houses = houses;
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (IMidgetHouse house in houses)
                {
                    string name = house.GetType().Name;
                    int count = house.MidgetsCount;
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}, Count: {1}", name, count));
                }
                Console.ResetColor();
                Thread.Sleep(500);
            }
        }
    }
}

namespace Restaurant.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Linq;

    using Restaurant.Events;

    public class AlarmClock : IEventHandler<IEvent>, IStartable
    {
        private readonly SortedList<DateTime, IEvent> alarms = new SortedList<DateTime, IEvent>(); 

        public void Handle(IEvent @event)
        {
            lock (alarms)
            {
                var e = @event as SendToMeIn;
                if (e != null)
                {
                    alarms.Add(e.WhenToSend, e.WhatToSend);
                }
            }
        }

        public string Name { get; private set; }

        public void Start()
        {
            Thread thread = new Thread(Work);
            thread.Start();
        }

        private void Work()
        {
            while (true)
            {
                lock (alarms)
                {
                    alarms.Where(x => x.Key <= DateTime.Now).ToList().ForEach(
                        x =>
                            {
                                Dispatcher.Instance.Publish(x.Value);
                                alarms.Remove(x.Key);
                            });
                }
                Thread.Sleep(1000);
            }
        }
    }
}
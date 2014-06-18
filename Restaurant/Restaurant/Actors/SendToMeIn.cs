namespace Restaurant.Actors
{
    using System;

    using Restaurant.Events;

    public class SendToMeIn : IEvent
    {
        public SendToMeIn(Guid correlationId, DateTime whenToSend, IEvent whatToSend)
        {
            CorrelationId = correlationId;
            WhenToSend = whenToSend;
            WhatToSend = whatToSend;
        }

        public IEvent WhatToSend { get; private set; }

        public DateTime WhenToSend { get; set; }

        public DateTime? TimeToLive { get; set; }

        public Guid Id { get; set; }

        public Guid CorrelationId { get; set; }

        public Guid CausationId { get; set; }
    }
}
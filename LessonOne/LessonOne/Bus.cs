namespace LessonOne
{
    using System;
    using System.Collections.Generic;

    public class Bus : IMessagePublisher
    {
        private Dictionary<Type, Func<IMessage>> _handlers = new Dictionary<Type, Func<IMessage>>();

        public void AddHandler(Type type, Func<IMessage> handler)
        {
            _handlers.Add(type, handler);
        }

        public void Publish(IMessage message)
        {
        }

        public void Publish(IComposeMessage message)
        {
        }
    }
}
namespace LessonOne
{
    using System.Collections.Generic;

    public class FakePublisher : IMessagePublisher
    {
        public List<IMessage> PublishedMessages { get; private set; }
        //public List<IComposeMessage> 

        public FakePublisher()
        {
            PublishedMessages = new List<IMessage>();
        }

        public void Publish(IMessage message)
        {
            PublishedMessages.Add(message);
        }

        public void Publish(IComposeMessage message)
        {
            PublishedMessages.Add(message);
        }
    }
}
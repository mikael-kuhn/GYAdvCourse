namespace LessonOne
{
    public interface IMessagePublisher
    {
        void Publish(IMessage message);

        void Publish(IComposeMessage message);
    }
}
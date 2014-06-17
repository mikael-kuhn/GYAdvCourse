namespace LessonOne
{
    public interface IHandleMessage<T> where T: IMessage
    {
        void Hande(T message);
    }
}
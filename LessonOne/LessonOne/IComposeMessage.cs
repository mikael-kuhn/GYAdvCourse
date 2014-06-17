namespace LessonOne
{
    using System;

    public interface IComposeMessage : IMessage
    {
        TimeSpan Seconds { get; set; }
        IMessage Message { get; set; }
 
    }
}
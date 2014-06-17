using System;

namespace LessonOne
{
    public sealed class SendToMeIn : IMessage
    {

        public SendToMeIn(TimeSpan seconds, IMessage message)
        {
            Seconds = seconds;
            Message = message;
        }

        public TimeSpan Seconds { get; private set; }

        public IMessage Message { get; private set; }
    }
}

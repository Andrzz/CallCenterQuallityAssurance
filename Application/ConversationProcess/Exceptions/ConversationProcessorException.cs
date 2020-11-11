namespace Application.ConversationProcess.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ConversationProcessorException : Exception
    {
        protected ConversationProcessorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ConversationProcessorException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}

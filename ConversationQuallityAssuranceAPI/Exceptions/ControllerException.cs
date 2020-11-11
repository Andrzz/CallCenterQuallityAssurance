namespace ConversationQuallityAssuranceAPI.Exception
{
    using System.Runtime.Serialization;
    using System;
    [Serializable]
    public class ControllerException : Exception
    {
        protected ControllerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ControllerException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}

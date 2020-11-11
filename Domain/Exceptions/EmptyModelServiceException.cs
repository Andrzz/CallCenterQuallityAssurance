namespace Domain.Exceptions
{
    using System.Runtime.Serialization;
    using System;
    [Serializable]
    public class EmptyModelServiceException : Exception
    {
        protected EmptyModelServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public EmptyModelServiceException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}

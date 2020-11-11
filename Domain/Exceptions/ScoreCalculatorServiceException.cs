namespace Domain.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ScoreCalculatorServiceException : Exception
    {
        protected ScoreCalculatorServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ScoreCalculatorServiceException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}

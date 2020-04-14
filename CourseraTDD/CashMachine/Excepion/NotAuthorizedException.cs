using System;
using System.Runtime.Serialization;

namespace CourseraTDD.CashMachine.Exception
{
    [Serializable]
    public class NotAuthorizedException : System.Exception
    {
        public NotAuthorizedException() : base("No permission to do the action.")
        {
        }

        public NotAuthorizedException(string message) : base(message)
        {
        }

        public NotAuthorizedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
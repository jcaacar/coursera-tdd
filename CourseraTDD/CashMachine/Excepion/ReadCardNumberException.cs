using System;
using System.Runtime.Serialization;

namespace CourseraTDDTEST.CashMachine.Exception
{
    [Serializable]
    public class ReadCardNumberException : System.Exception
    {
        public ReadCardNumberException() : base("Error when try read card number.")
        {
        }

        public ReadCardNumberException(string message) : base(message)
        {
        }

        public ReadCardNumberException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ReadCardNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace CourseraTDD.CashMachine.Exception
{
    [Serializable]
    internal class InsufficientBalanceException : System.Exception
    {
        public InsufficientBalanceException()
        {
        }

        public InsufficientBalanceException(string message) : base(message)
        {
        }

        public InsufficientBalanceException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
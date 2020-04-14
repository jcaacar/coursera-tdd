using System;
using System.Runtime.Serialization;

namespace CourseraTDD.CashMachine.Exception
{
    [Serializable]
    public class ReadValueToWithdrawException : System.Exception
    {
        public ReadValueToWithdrawException()
        {
        }

        public ReadValueToWithdrawException(string message) : base(message)
        {
        }

        public ReadValueToWithdrawException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ReadValueToWithdrawException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
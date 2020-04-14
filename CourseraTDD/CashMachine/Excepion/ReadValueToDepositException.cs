using System;
using System.Runtime.Serialization;

namespace CourseraTDD.CashMachine.Exception
{
    [Serializable]
    public class ReadValueToDepositException : System.Exception
    {
        public ReadValueToDepositException()
        {
        }

        public ReadValueToDepositException(string message) : base(message)
        {
        }

        public ReadValueToDepositException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ReadValueToDepositException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
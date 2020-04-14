using System;
using System.Runtime.Serialization;

namespace CourseraTDD.CashMachine.Exception
{
    [Serializable]
    public class AccountPersistenceException : System.Exception
    {
        public AccountPersistenceException() : base("Error to try persist account.")
        {
        }

        public AccountPersistenceException(string message) : base(message)
        {
        }

        public AccountPersistenceException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected AccountPersistenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
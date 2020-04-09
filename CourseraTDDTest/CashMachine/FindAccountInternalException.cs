﻿using System;
using System.Runtime.Serialization;

namespace CourseraTDDTEST.CashMachine
{
    [Serializable]
    internal class FindAccountInternalException : System.Exception
    {
        public FindAccountInternalException()
        {
        }

        public FindAccountInternalException(string message) : base(message)
        {
        }

        public FindAccountInternalException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected FindAccountInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
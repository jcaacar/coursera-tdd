using CourseraTDD.CashMachine;
using CourseraTDD.CashMachine.Exception;

namespace CourseraTDDTEST.CashMachine
{
    public class HardwareMock : MockBase, IHardware
    {
        public enum Error
        {
            None = 0,
            GetCardNumber = 1,
            ValueToDeposit = 2,
            ValueToWithdraw = 4
        }

        public const string MethodGetCardNumber = nameof(GetCardNumber);
        public const string MethodGetValueToDeposit = nameof(GetValueToDeposit);
        public const string MethodGetValueToWithdraw = nameof(GetValueToWithdraw);

        private readonly string cardNumber;
        private readonly decimal valueToDeposit;
        private readonly decimal valueToWithdraw;
        private readonly Error errorFlags;

        public HardwareMock(string cardNumber, Error errorFlags = 0) : this(cardNumber, 0, errorFlags)
        {
        }

        public HardwareMock(string cardNumber, decimal valueToDeposit, Error errorFlags = 0) : this(cardNumber, valueToDeposit, 0m, errorFlags)
        {
            this.cardNumber = cardNumber;
            this.valueToDeposit = valueToDeposit;
            this.errorFlags = errorFlags;
        }

        public HardwareMock(string cardNumber, decimal valueToDeposit, decimal valueToWithdraw, Error errorFlags = 0)
        {
            this.cardNumber = cardNumber;
            this.valueToDeposit = valueToDeposit;
            this.valueToWithdraw = valueToWithdraw;
            this.errorFlags = errorFlags;
        }

        public string GetCardNumber()
        {
            var exception = (errorFlags & Error.GetCardNumber) != 0 ? new ReadCardNumberException() : null;

            if (exception != null)
            {
                AddMethodCalled(MethodGetCardNumber, null, exception);
                throw exception;
            }

            AddMethodCalled(MethodGetCardNumber, null, cardNumber);

            return cardNumber;
        }

        public decimal GetValueToDeposit()
        {
            var exception = (errorFlags & Error.ValueToDeposit) != 0 ? new ReadValueToDepositException() : null;

            if (exception != null)
            {
                AddMethodCalled(MethodGetValueToDeposit, null, exception);
                throw exception;
            }

            AddMethodCalled(MethodGetValueToDeposit, null, valueToDeposit);

            return valueToDeposit;
        }

        public decimal GetValueToWithdraw()
        {
            var exception = (errorFlags & Error.ValueToWithdraw) != 0 ? new ReadValueToWithdrawException() : null;

            if (exception != null)
            {
                AddMethodCalled(MethodGetValueToWithdraw, null, exception);
                throw exception;
            }

            AddMethodCalled(MethodGetValueToWithdraw, null, valueToWithdraw);

            return valueToWithdraw;
        }
    }
}

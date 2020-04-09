using CourseraTDD.CashMachine.Exception;

namespace CourseraTDD.CashMachine
{
    public class Account
    {
        public decimal Balance { get; private set; }

        public string Number { get; private set; }

        public Account(string number)
        {
            Number = number;
        }

        public void Deposit(decimal value)
        {
            Balance += value;
        }

        public void Withdraw(decimal value)
        {
            if (Balance < value)
            {
                throw new InsufficientBalanceException();
            }

            Balance -= value;
        }
    }
}
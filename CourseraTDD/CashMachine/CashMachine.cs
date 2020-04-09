using CourseraTDD.CashMachine.Exception;
using CourseraTDDTEST.CashMachine.Exception;

namespace CourseraTDD.CashMachine
{
    public class CashMachine
    {
        public const string SuccessLoginMessage = "Authenticated User";
        public const string InvalidLoginMessage = "The User Could Not Be Authenticated";
        public const string SuccessDepositMessage = "Deposit Received Successfully";
        public const string SuccessBalanceMessage = "The Account Balance is R$ {0}";
        public const string SuccessWithdrawMessage = "Retire seu dinheiro";
        public const string InsufficientBalanceMessage = "Saldo insuficiente";

        private readonly IHardware hardware;
        private readonly IRemoteService remoteService;

        private Account account;

        public CashMachine(IHardware hardware, IRemoteService remoteService)
        {
            this.hardware = hardware;
            this.remoteService = remoteService;
        }

        public string Login()
        {
            var number = hardware.GetCardNumber();

            account = remoteService.FindAccount(number);

            return account != null ? SuccessLoginMessage : InvalidLoginMessage;
        }

        public string AccountBalance()
        {
            CheckAuthentication();

            return string.Format(SuccessBalanceMessage, account.Balance);
        }

        public string Deposit()
        {
            CheckAuthentication();

            var value = hardware.GetValueToDeposit();

            account.Deposit(value);

            remoteService.PersistAccount(account);

            return SuccessDepositMessage;
        }

        public string Withdraw()
        {
            try
            {
                var value = hardware.GetValueToWithdraw();

                account.Withdraw(value);

                remoteService.PersistAccount(account);

                return SuccessWithdrawMessage;
            }
            catch (InsufficientBalanceException)
            {
                return InsufficientBalanceMessage;
            }
        }

        private void CheckAuthentication()
        {
            if (account == null)
                throw new NotAuthorizedException();
        }
    }
}

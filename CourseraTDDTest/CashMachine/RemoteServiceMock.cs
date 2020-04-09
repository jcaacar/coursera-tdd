using CourseraTDD.CashMachine;
using CourseraTDDTEST.CashMachine.Exception;

namespace CourseraTDDTEST.CashMachine
{
    public class RemoteServiceMock : MockBase, IRemoteService
    {
        public enum Error
        {
            None = 0,
            FindAccountNotExists = 1,
            FindAccountInternal = 2,
            PersistAccount = 4
        }

        public const string MethodFindAccount = nameof(FindAccount);
        public const string MethodPersitAccount = nameof(PersistAccount);

        private readonly Account account;
        private Error errorFlags = Error.None;

        public RemoteServiceMock(Account account)
        {
            this.account = account;
        }

        public RemoteServiceMock(Account account, Error errorFlags)
        {
            this.account = account;
            this.errorFlags = errorFlags;
        }

        public void setError(Error error)
        {
            errorFlags |= error;
        }

        public Account FindAccount(string number)
        {
            if ((errorFlags & Error.FindAccountInternal) != 0) throw new FindAccountInternalException();

            var result = errorFlags.HasFlag(Error.FindAccountNotExists) ? null : account;

            AddMethodCalled(MethodFindAccount, new object[] { number }, result);

            return result;
        }

        public void PersistAccount(Account account)
        {
            var exception = (errorFlags & Error.PersistAccount) != 0 ? new AccountPersistenceException() : null;

            AddMethodCalled(MethodPersitAccount, new object[] { account }, exception);

            if (exception != null) throw new AccountPersistenceException();
        }
    }
}
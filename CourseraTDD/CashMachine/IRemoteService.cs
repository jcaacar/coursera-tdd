namespace CourseraTDD.CashMachine
{
    public interface IRemoteService
    {
        Account FindAccount(string number);

        void PersistAccount(Account account);
    }
}

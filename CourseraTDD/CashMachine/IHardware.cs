namespace CourseraTDD.CashMachine
{
    public interface IHardware
    {
        string GetCardNumber();

        decimal GetValueToDeposit();

        decimal GetValueToWithdraw();
    }
}
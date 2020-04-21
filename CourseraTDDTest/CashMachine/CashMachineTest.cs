using CourseraTDD.CashMachine;
using CourseraTDD.CashMachine.Exception;
using NUnit.Framework;
using cashMachine = CourseraTDD.CashMachine;

namespace CourseraTDDTEST.CashMachine
{
    [TestFixture]
    public class CashMachineTest
    {
        private const string number = "19101010";

        private Account account;

        [SetUp]
        public void Setup()
        {
            account = new Account(number);
        }

        #region Login

        [Test]
        public void LoginWithAccountNumberValid()
        {
            var hardwareMock = new HardwareMock(number);
            var serviceMock = new RemoteServiceMock(account);

            var machine = new cashMachine.CashMachine(hardwareMock, serviceMock);

            var result = machine.Login();

            hardwareMock.VerifyCalledMethod(HardwareMock.MethodGetCardNumber, null, number);
            serviceMock.VerifyCalledMethod(RemoteServiceMock.MethodFindAccount, new object[] { number }, account);

            Assert.AreEqual(cashMachine.CashMachine.SuccessLoginMessage, result);
        }

        [Test]
        public void LoginWithAccountNumberInvalid()
        {
            var hardwareMock = new HardwareMock(number);
            var serviceMock = new RemoteServiceMock(account, RemoteServiceMock.Error.FindAccountNotExists);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            var result = cashMachine.Login();

            hardwareMock.VerifyCalledMethod(HardwareMock.MethodGetCardNumber, null, number);
            serviceMock.VerifyCalledMethod(RemoteServiceMock.MethodFindAccount, new object[] { number });

            Assert.AreEqual(CourseraTDD.CashMachine.CashMachine.InvalidLoginMessage, result);
        }

        [Test]
        public void LoginErrorWhenCantGetCardNumberFromHardware()
        {
            var hardwareMock = new HardwareMock(number, HardwareMock.Error.GetCardNumber);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, null);

            Assert.Throws<ReadCardNumberException>(() => cashMachine.Login());
        }

        [Test]
        public void LoginErrorWhenCantFindAccount()
        {
            var hardwareMock = new HardwareMock(number);

            var serviceMock = new RemoteServiceMock(account, RemoteServiceMock.Error.FindAccountInternal);
            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);

            Assert.Throws<FindAccountInternalException>(() => cashMachine.Login());
        }

        #endregion

        #region Deposit

        [Test]
        public void DepositAuthenticated()
        {
            var valueToDeposit = 100.5m;

            var hardwareMock = new HardwareMock(number, valueToDeposit);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();

            var result = cashMachine.Deposit();

            hardwareMock.VerifyCalledMethod(HardwareMock.MethodGetValueToDeposit, null, valueToDeposit);
            serviceMock.VerifyCalledMethod(RemoteServiceMock.MethodPersitAccount, new object[] { account });

            Assert.AreEqual(CourseraTDD.CashMachine.CashMachine.SuccessDepositMessage, result);
        }

        [Test]
        public void DepositNotAuthenticated()
        {
            var cashMachine = new cashMachine.CashMachine(null, null);

            Assert.Throws<NotAuthorizedException>(() => cashMachine.Deposit());
        }


        [Test]
        public void DepositErrorWhenCantGetValueFromHardware()
        {
            var valueToDeposit = 100.5m;

            var hardwareMock = new HardwareMock(number, valueToDeposit, HardwareMock.Error.ValueToDeposit);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();

            Assert.Throws<ReadValueToDepositException>(() => cashMachine.Deposit());
        }

        [Test]
        public void DepositWithErrorInRemoteService()
        {
            var valueToDeposit = 100.5m;

            var hardwareMock = new HardwareMock(number, valueToDeposit);
            var serviceMock = new RemoteServiceMock(account, RemoteServiceMock.Error.PersistAccount);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();

            Assert.Throws<AccountPersistenceException>(() => cashMachine.Deposit());
        }

        #endregion

        #region Balance


        [Test]
        public void RequestEmptyBalance()
        {
            var hardwareMock = new HardwareMock(number);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();

            var ballance = cashMachine.AccountBalance();

            Assert.AreEqual(string.Format(CourseraTDD.CashMachine.CashMachine.SuccessBalanceMessage, 0), ballance);
        }

        [Test]
        public void RequestBalanceWithValue()
        {
            var depositValue = 50.5m;

            var hardwareMock = new HardwareMock(number, depositValue);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();
            cashMachine.Deposit();

            var ballance = cashMachine.AccountBalance();

            Assert.AreEqual(string.Format(CourseraTDD.CashMachine.CashMachine.SuccessBalanceMessage, depositValue), ballance);
        }

        [Test]
        public void RequestBalanceNotAuthenticated()
        {
            var cashMachine = new cashMachine.CashMachine(null, null);

            Assert.Throws<NotAuthorizedException>(() => cashMachine.AccountBalance());
        }

        #endregion

        #region Withdraw

        [Test]
        public void WithdrawWithBalancePositive()
        {
            var depositValue = 50.5m;
            var withdrawValue = 10.2m;

            var expectedBalance = depositValue - withdrawValue;

            var hardwareMock = new HardwareMock(number, depositValue, withdrawValue);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();
            cashMachine.Deposit();

            var result = cashMachine.Withdraw();

            var balance = cashMachine.AccountBalance();

            hardwareMock.VerifyCalledMethod(HardwareMock.MethodGetValueToWithdraw, null, withdrawValue);
            serviceMock.VerifyCalledMethod(RemoteServiceMock.MethodPersitAccount, new object[] { account }, null, 2);

            Assert.AreEqual(string.Format(CourseraTDD.CashMachine.CashMachine.SuccessBalanceMessage, expectedBalance), balance);
            Assert.AreEqual(CourseraTDD.CashMachine.CashMachine.SuccessWithdrawMessage, result);
        }

        [Test]
        public void WithdrawWithBalanceInsufficient()
        {
            var depositValue = 50.5m;
            var withdrawValue = 100.2m;

            var hardwareMock = new HardwareMock(number, depositValue, withdrawValue);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();
            cashMachine.Deposit();

            var result = cashMachine.Withdraw();

            var balance = cashMachine.AccountBalance();

            hardwareMock.VerifyCalledMethod(HardwareMock.MethodGetValueToWithdraw, null, withdrawValue);
            serviceMock.VerifyCalledMethod(RemoteServiceMock.MethodPersitAccount, new object[] { account });

            Assert.AreEqual(string.Format(CourseraTDD.CashMachine.CashMachine.SuccessBalanceMessage, depositValue), balance);
            Assert.AreEqual(CourseraTDD.CashMachine.CashMachine.InsufficientBalanceMessage, result);
        }

        [Test]
        public void WithdrawWhenCantGetValueFromHardware()
        {
            var depositValue = 50.5m;

            var hardwareMock = new HardwareMock(number, depositValue, 0m, HardwareMock.Error.ValueToWithdraw);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();
            cashMachine.Deposit();

            Assert.Throws<ReadValueToWithdrawException>(() => cashMachine.Withdraw());
        }

        [Test]
        public void WithdrawWithErrorInRemoteService()
        {
            var depositValue = 50.5m;
            var withdrawValue = 10.2m;

            var hardwareMock = new HardwareMock(number, depositValue, withdrawValue);
            var serviceMock = new RemoteServiceMock(account);

            var cashMachine = new cashMachine.CashMachine(hardwareMock, serviceMock);
            cashMachine.Login();
            cashMachine.Deposit();

            serviceMock.SetError(RemoteServiceMock.Error.PersistAccount);

            Assert.Throws<AccountPersistenceException>(() => cashMachine.Withdraw());
        }

        #endregion

    }
}

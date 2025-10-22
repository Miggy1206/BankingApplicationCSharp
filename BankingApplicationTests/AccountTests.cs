using BankingApplicationClassLibrary;
using System;
using System.ComponentModel;

namespace BankingApplicationTests
{
    public class AccountTests
    {
        [Trait("Category","CurrentAccount")]
        [Fact]
        public void Deposit_Positive_Amount()
        {
            var acct = new CurrentAccount();
            acct.balance = 100.00;

            acct.Deposit(50.00);

            Assert.Equal(150.00, acct.balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Deposit_Non_Positive_Amount()
        {
            var acct = new CurrentAccount();
            acct.balance = 0.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Positive_Amount_No_Overdraft_Within_Limit()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 200.0;
            acct.overdraftLimit = 0.0;

            // Act
            acct.Withdraw(50.0);

            // Assert
            Assert.Equal(150.00, acct.balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Positive_Amount_With_Overdraft_Within_Limit()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.00;
            acct.overdraftLimit = 50.00;

            // Act
            acct.Withdraw(140.00);

            // Assert
            Assert.Equal(-40.00, acct.balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Amount_Equal_Balance_With_Overdraft()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.00;
            acct.overdraftLimit = 50.00;

            // Act
            acct.Withdraw(150.00); // exactly balance + overdraft

            // Assert
            Assert.Equal(-50.00, acct.balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Amount_Exceeds_Balance_With_Overdraft()
        {
            var acct = new CurrentAccount();
            acct.balance = 100.00;
            acct.overdraftLimit = 30.00;

            var ex = Assert.Throws<InsufficentBalanceException>(() => acct.Withdraw(200.0)); // 200 > 130
            Assert.Contains("Insufficient funds", ex.Message);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Non_Positive_Amount()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.00;
            acct.overdraftLimit = 50.00;

            // Act & Assert
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);
        }
    }
}

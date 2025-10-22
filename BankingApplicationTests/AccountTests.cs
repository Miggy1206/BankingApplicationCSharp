using BankingApplicationClassLibrary;
using System;

namespace BankingApplicationTests
{
    public class AccountTests
    {
        [Fact]
        public void Deposit_PositiveAmount_IncreasesBalance()
        {
            var acct = new CurrentAccount();
            acct.balance = 100.00;

            // Act
            acct.Deposit(50.0);

            // Assert
            Assert.Equal(150.0, acct.balance, 3);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Deposit_NonPositiveAmount_ThrowsArgumentException(double amount)
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 0.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => acct.Deposit(amount));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Fact]
        public void Withdraw_PositiveAmount_DecreasesBalance()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 200.0;
            acct.overdraftLimit = 0.0;

            // Act
            acct.Withdraw(50.0);

            // Assert
            Assert.Equal(150.0, acct.balance, 3);
        }

        [Fact]
        public void Withdraw_WithinOverdraft_AllowsNegativeBalanceWithinLimit()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.0;
            acct.overdraftLimit = 50.0;

            // Act
            acct.Withdraw(140.0);

            // Assert
            Assert.Equal(-40.0, acct.balance, 3);
        }

        [Fact]
        public void Withdraw_EqualToBalancePlusOverdraft_IsAllowed()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.0;
            acct.overdraftLimit = 50.0;

            // Act
            acct.Withdraw(150.0); // exactly balance + overdraft

            // Assert
            Assert.Equal(-50.0, acct.balance, 3);
        }

        [Fact]
        public void Withdraw_ExceedsBalanceAndOverdraft_ThrowsInvalidOperationException()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.0;
            acct.overdraftLimit = 30.0;

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => acct.Withdraw(200.0)); // 200 > 130
            Assert.Contains("Insufficient funds", ex.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Withdraw_NonPositiveAmount_ThrowsArgumentException(double amount)
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.balance = 100.0;
            acct.overdraftLimit = 50.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => acct.Withdraw(amount));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);
        }
    }
}

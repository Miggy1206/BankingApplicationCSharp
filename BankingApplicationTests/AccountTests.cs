using BankingApplicationClassLibrary;
using System;
using System.ComponentModel;

namespace BankingApplicationTests
{
    public class AccountTests
    {
        //Current Account Tests

        [Trait("Category", "CurrentAccount")]
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



        //Savings Account Tests

        [Trait("Category", "SavingsAccount")]
        [Fact]
        public void Savings_Deposit_Positive_Amount()
        {
            var acct = new SavingsAccount();
            acct.accountType = AccountType.InstantAccess;
            acct.balance = 100.00;

            acct.Deposit(50.00);

            Assert.Equal(150.00, acct.balance, 3);
        }

        [Trait("Category", "SavingsAccount")]
        [Fact]
        public void Savings_Deposit_Non_Positive_Amount()
        {
            var acct = new SavingsAccount();
            acct.accountType = AccountType.InstantAccess;
            acct.balance = 0.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }


        //Instant Savings Account Tests

        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Positive_Amount_Within_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.accountType = AccountType.InstantAccess;
            acct.balance = 200.0;


            // Act
            acct.Withdraw(50.0);

            // Assert
            Assert.Equal(150.00, acct.balance, 3);
        }

        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Negative_Amount()
        {
            var acct = new SavingsAccount();
            acct.accountType = AccountType.InstantAccess;
            acct.balance = 200.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);
        }

        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Positive_Amount_Equal_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.accountType = AccountType.InstantAccess;
            acct.balance = 200.0;

            // Act
            acct.Withdraw(200.0);

            // Assert
            Assert.Equal(0.00, acct.balance, 3);
        }


        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Positive_Amount_Exceeds_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.accountType = AccountType.InstantAccess;
            acct.balance = 100.0;

            // Assert
            var ex = Assert.Throws<InsufficentBalanceException>(() => acct.Withdraw(200.0)); // 200 > 130
            Assert.Contains("Insufficient funds", ex.Message);
        }



        //Fixed Savings Account Tests

        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Positive_Amount_Within_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.accountType = AccountType.FixedTerm;
            acct.balance = 200.0;


            var ex = Assert.Throws<InvalidAccountTypeWithdrawalException>(() => acct.Withdraw(100));
            Assert.Contains("You can not withdraw of this account.", ex.Message);
        }

        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Negative_Amount()
        {
            var acct = new SavingsAccount();
            acct.accountType = AccountType.FixedTerm;
            acct.balance = 200.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);
        }

        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Positive_Amount_Equal_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.accountType = AccountType.FixedTerm;
            acct.balance = 200.0;

            var ex = Assert.Throws<InvalidAccountTypeWithdrawalException>(() => acct.Withdraw(200));
            Assert.Contains("You can not withdraw of this account.", ex.Message);
        }


        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Positive_Amount_Exceeds_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.accountType = AccountType.FixedTerm;
            acct.balance = 100.0;

            // Assert
            var ex = Assert.Throws<InsufficentBalanceException>(() => acct.Withdraw(200.0));
            Assert.Contains("Insufficient funds", ex.Message);
        }


        //Credit card Account Tests
        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Deposit_Positive_Amount()
        {
            var acct = new CreditAccount();
            acct.balance = 500.00;
            acct.Deposit(200.00);
            Assert.Equal(300.00, acct.balance, 3);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Deposit_Non_Positive_Amount()
        {
            var acct = new CreditAccount();
            acct.balance = 500.00;
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Deposit_Positive_Amount_Exceeds_Debt()
        {
            var acct = new CreditAccount();
            acct.balance = 500.00;
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(600));
            Assert.Contains("Deposit can not exceed debt.", ex.Message);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Withdraw_Positive_Amount_Within_Limit()
        {
            var acct = new CreditAccount();
            acct.balance = 500.00;
            acct.creditLimit = 2000.00;
            acct.withdrawalFee = 10.00;

            acct.Withdraw(200.00);

            Assert.Equal(720, acct.balance, 3);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Withdraw_Negative_Amount()
        {
            var acct = new CreditAccount();
            acct.balance = 500.00;
            acct.creditLimit = 2000.00;
            acct.withdrawalFee = 10.00;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Withdraw_Positive_Amount_Exceed_Limits()
        {
            var acct = new CreditAccount();
            acct.balance = 500.00;
            acct.creditLimit = 2000.00;
            acct.withdrawalFee = 10.00;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(1600));
            Assert.Contains("Withdrawal amount + interest can not exceed limit.", ex.Message);

        }




    }
}

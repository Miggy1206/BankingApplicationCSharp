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
            acct.Balance = 100.00;

            acct.Deposit(50.00);

            Assert.Equal(150.00, acct.Balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Deposit_Non_Positive_Amount()
        {
            var acct = new CurrentAccount();
            acct.Balance = 0.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Positive_Amount_No_Overdraft_Within_Limit()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.Balance = 200.0;
            acct.OverdraftLimit = 0.0;

            // Act
            acct.Withdraw(50.0);

            // Assert
            Assert.Equal(150.00, acct.Balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Positive_Amount_With_Overdraft_Within_Limit()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.Balance = 100.00;
            acct.OverdraftLimit = 50.00;

            // Act
            acct.Withdraw(140.00);

            // Assert
            Assert.Equal(-40.00, acct.Balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Amount_Equal_Balance_With_Overdraft()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.Balance = 100.00;
            acct.OverdraftLimit = 50.00;

            // Act
            acct.Withdraw(150.00); // exactly balance + overdraft

            // Assert
            Assert.Equal(-50.00, acct.Balance, 3);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Amount_Exceeds_Balance_With_Overdraft()
        {
            var acct = new CurrentAccount();
            acct.Balance = 100.00;
            acct.OverdraftLimit = 30.00;

            var ex = Assert.Throws<InsufficentBalanceException>(() => acct.Withdraw(200.0)); // 200 > 130
            Assert.Contains("Insufficient funds", ex.Message);
        }

        [Trait("Category", "CurrentAccount")]
        [Fact]
        public void Withdraw_Non_Positive_Amount()
        {
            // Arrange
            var acct = new CurrentAccount();
            acct.Balance = 100.00;
            acct.OverdraftLimit = 50.00;

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
            acct.AccountType = AccountTypes.InstantAccess;
            acct.Balance = 100.00;

            acct.Deposit(50.00);

            Assert.Equal(150.00, acct.Balance, 3);
        }

        [Trait("Category", "SavingsAccount")]
        [Fact]
        public void Savings_Deposit_Non_Positive_Amount()
        {
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.InstantAccess;
            acct.Balance = 0.0;

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
            acct.AccountType = AccountTypes.InstantAccess;
            acct.Balance = 200.0;


            // Act
            acct.Withdraw(50.0);

            // Assert
            Assert.Equal(150.00, acct.Balance, 3);
        }

        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Negative_Amount()
        {
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.InstantAccess;
            acct.Balance = 200.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);
        }

        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Positive_Amount_Equal_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.InstantAccess;
            acct.Balance = 200.0;

            // Act
            acct.Withdraw(200.0);

            // Assert
            Assert.Equal(0.00, acct.Balance, 3);
        }


        [Trait("Category", "InstantSavingsAccount")]
        [Fact]
        public void Instant_Savings_Withdraw_Positive_Amount_Exceeds_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.InstantAccess;
            acct.Balance = 100.0;

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
            acct.AccountType = AccountTypes.FixedTerm;
            acct.Balance = 200.0;


            var ex = Assert.Throws<InvalidAccountTypeWithdrawalException>(() => acct.Withdraw(100));
            Assert.Contains("You can not withdraw of this account.", ex.Message);
        }

        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Negative_Amount()
        {
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.FixedTerm;
            acct.Balance = 200.0;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);
        }

        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Positive_Amount_Equal_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.FixedTerm;
            acct.Balance = 200.0;

            var ex = Assert.Throws<InvalidAccountTypeWithdrawalException>(() => acct.Withdraw(200));
            Assert.Contains("You can not withdraw of this account.", ex.Message);
        }


        [Trait("Category", "FixedSavingsAccount")]
        [Fact]
        public void Fixed_Savings_Withdraw_Positive_Amount_Exceeds_Limit()
        {
            // Arrange
            var acct = new SavingsAccount();
            acct.AccountType = AccountTypes.FixedTerm;
            acct.Balance = 100.0;

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
            acct.Balance = 500.00;
            acct.Deposit(200.00);
            Assert.Equal(300.00, acct.Balance, 3);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Deposit_Non_Positive_Amount()
        {
            var acct = new CreditAccount();
            acct.Balance = 500.00;
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Deposit_Positive_Amount_Exceeds_Debt()
        {
            var acct = new CreditAccount();
            acct.Balance = 500.00;
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(600));
            Assert.Contains("Deposit can not exceed debt.", ex.Message);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Withdraw_Positive_Amount_Within_Limit()
        {
            var acct = new CreditAccount();
            acct.Balance = 500.00;
            acct.CreditLimit = 2000.00;
            acct.WithdrawalFee = 10.00;

            acct.Withdraw(200.00);

            Assert.Equal(720.00, acct.Balance, 2);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Withdraw_Negative_Amount()
        {
            var acct = new CreditAccount();
            acct.Balance = 500.00;
            acct.CreditLimit = 2000.00;
            acct.WithdrawalFee = 10.00;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);

        }

        [Trait("Category", "CreditCardAccount")]
        [Fact]
        public void CreditCard_Withdraw_Positive_Amount_Exceed_Limits()
        {
            var acct = new CreditAccount();
            acct.Balance = 500.00;
            acct.CreditLimit = 2000.00;
            acct.WithdrawalFee = 10.00;

            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(1600));
            Assert.Contains("Withdrawal amount + interest can not exceed limit.", ex.Message);

        }


        //Instant Mortgage Account Tests

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Deposit_Positive_Amount()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.Balance = 150000.00;
            acct.Deposit(300.00);
            Assert.Equal(149700.00, acct.Balance, 2);
        }

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Deposit_Negative_Amount()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.Balance = 150000.00;


            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Deposit_Balance_Equal_Amount()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.Balance = 150000.00;
            acct.Deposit(150000.00);
            Assert.Equal(0, acct.Balance, 2);
        }

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Deposit_Amount_Exceeds_Limit()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.Balance = 150000.00;


            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(160000));
            Assert.Contains("Deposit can not exceed debt.", ex.Message);
        }


        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Withdraw_Positive_Amount_Below_Total_Mortgage_Borrowing()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;
            acct.Withdraw(30000.00);
            Assert.Equal(180000, acct.Balance, 2);
        }

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Withdraw_Negative_Amount()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;


            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);

        }

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Withdraw_Amount_Equal_Total_Mortgage_Borrowing()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;
            acct.Withdraw(50000.00);
            Assert.Equal(200000, acct.Balance, 2);
        }

        [Trait("Category", "InstantMortgageAccount")]
        [Fact]
        public void Instant_Mortgage_Withdraw_Amount_Exceeds_Total_Mortgage_Borrowing()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.InstantAccess;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(60000));
            Assert.Contains("Withdrawal can not exceed total mortgage borrowing.", ex.Message);
        }



        //Fixed Mortgage Account Tests

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Deposit_Positive_Amount_Below_Overpayment_Limit()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.fixedOverpaymentLimit = 10;
            acct.Balance = 150000.00;
            acct.Deposit(300.00);
            Assert.Equal(149700, acct.Balance, 2);
        }

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Deposit_Negative_Amount()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.fixedOverpaymentLimit = 10;
            acct.Balance = 150000.00;


            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(-100));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Deposit_Amount_Equal_Overpayment_Limit()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.fixedOverpaymentLimit = 10;
            acct.Balance = 150000.00;
            acct.Deposit(15000.00);
            Assert.Equal(135000, acct.Balance, 2);
        }

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Deposit_Amount_Exceeds_Overpayment_Limit()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.fixedOverpaymentLimit = 10;
            acct.Balance = 150000.00;


            var ex = Assert.Throws<InvalidAmountException>(() => acct.Deposit(20000));
            Assert.Contains("Deposit can not exceed overpayment limit (10% of balance)", ex.Message);
        }





        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Withdraw_Positive_Amount_Below_Total_Mortgage_Borrowing()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;
            acct.Withdraw(30000.00);
            Assert.Equal(180000, acct.Balance, 2);
        }

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Withdraw_Negative_Amount()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;


            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(-100));
            Assert.Contains("Withdrawal amount must be positive", ex.Message);

        }

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Withdraw_Amount_Equal_Total_Mortgage_Borrowing()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;
            acct.Withdraw(50000.00);
            Assert.Equal(200000, acct.Balance, 2);
        }

        [Trait("Category", "FixedMortgageAccount")]
        [Fact]
        public void Fixed_Mortgage_Withdraw_Amount_Exceeds_Total_Mortgage_Borrowing()
        {
            var acct = new MortgageAccount();
            acct.mortgageType = AccountTypes.FixedTerm;
            acct.totalMortgageAmount = 200000.00;
            acct.Balance = 150000.00;
            var ex = Assert.Throws<InvalidAmountException>(() => acct.Withdraw(60000));
            Assert.Contains("Withdrawal can not exceed total mortgage borrowing.", ex.Message);
        }



    }
}

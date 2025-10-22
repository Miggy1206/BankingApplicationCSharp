namespace BankingApplicationClassLibrary
{
    public enum AccountType
    {
        InstantAccess,
        FixedTerm
    }

    interface IAccount
    {
        void Deposit(double amount);
        void Withdraw(double amount);

    }

    public class Account
    {
        public string sortCode { get; set; }
        public string accountNumber { get; set; }
        public string customerID { get; set; }
        public double balance { get; set; }
        public DateTime openedDate { get; set; }
        public string openedByStaffID { get; set; }

    }


    public class SavingsAccount : Account, IAccount
    {
        public decimal interestRate;
        public DateTime interestDate;
        public DateTime maturityDate;
        public AccountType accountType;

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Deposit amount must be positive.");
            }
            this.balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            else if (amount > this.balance)
            {
                throw new InsufficentBalanceException("Insufficient funds.");
            }
            if(accountType == AccountType.FixedTerm)
            {
                throw new InvalidAccountTypeWithdrawalException("You can not withdraw of this account.");
            }

            this.balance -= amount;
        }
    }

    public class CurrentAccount : Account, IAccount
    {
        public double overdraftLimit { get; set; }
        public double overdraftInterestRate { get; set; }


        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Deposit amount must be positive.");
            }
            this.balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            else if (amount > this.balance + this.overdraftLimit)
            {
                throw new InsufficentBalanceException("Insufficient funds including overdraft limit.");
            }

            this.balance -= amount;
        }
    }

    public class CreditAccount : Account
    {
        public double creditLimit;
        public double creditInterestRate;
    }
    public class MortgageAccount : Account
    {
        public double totalMortgageAmount;
        public double mortgageInterestRate;
        public DateTime repaymentDate;
        public AccountType mortgageType;
    }
}

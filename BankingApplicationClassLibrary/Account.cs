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
        private string sortCode;
        private string accountNumber;
        private string customerID;
        private double balance;
        private DateTime openedDate;
        private string openedByStaffID;


        public string Sortcode
        {
            set { sortCode = value; }
            get { return sortCode; }
        }
        public string AccountNumber
        {
            set { accountNumber = value; }
            get { return accountNumber; }
        }
        public string CustomerID
        {
            set { customerID = value; }
            get { return customerID; }
        }

        public double Balance
        {
            set { balance = value; }
            get { return balance; }
        }
        public DateTime OpenedDate
        {
            set { openedDate = value; }
            get { return openedDate; }
        }
        public string OpenedByStaffID
        {
            set { openedByStaffID = value; }
            get { return openedByStaffID; }
        }

    }


    public class SavingsAccount : Account, IAccount
    {
        public double interestRate;
        public DateTime interestDate;
        public DateTime maturityDate;
        public AccountType accountType;

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Deposit amount must be positive.");
            }
            this.Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            else if (amount > this.Balance)
            {
                throw new InsufficentBalanceException("Insufficient funds.");
            }
            if(accountType == AccountType.FixedTerm)
            {
                throw new InvalidAccountTypeWithdrawalException("You can not withdraw of this account.");
            }

            this.Balance -= amount;
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
            this.Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            else if (amount > this.Balance + this.overdraftLimit)
            {
                throw new InsufficentBalanceException("Insufficient funds including overdraft limit.");
            }

            this.Balance -= amount;
        }
    }

    public class CreditAccount : Account, IAccount
    {
        public double creditLimit;
        public double creditInterestRate;
        public double withdrawalFee;
        public string accountNumber16;

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Deposit amount must be positive.");
            }
            else if (this.Balance - amount < 0)
            {
                throw new InvalidAmountException("Deposit can not exceed debt.");
            }
            this.Balance -= amount;
        }

        public void Withdraw(double amount)
        {
            double totalAmount = amount + (amount * withdrawalFee / 100);

            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            else if (this.Balance + totalAmount > creditLimit)
            {
                throw new InvalidAmountException("Withdrawal amount + interest can not exceed limit.");
            }
            this.Balance += totalAmount;
        }
    }
    public class MortgageAccount : Account, IAccount
    {
        public double totalMortgageAmount;
        public double mortgageInterestRate;
        public DateTime repaymentDate;
        public AccountType mortgageType;
        public double furtherAdvanceCharge;
        public double fixedOverpaymentLimit;
        public string accountNumber16;

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Deposit amount must be positive.");
            }
            else if (this.Balance - amount < 0)
            {
                throw new InvalidAmountException("Deposit can not exceed debt.");
            }
            if (mortgageType == AccountType.FixedTerm)
            {
                if (amount > Balance * fixedOverpaymentLimit / 100)
                {
                    throw new InvalidAmountException("Deposit can not exceed overpayment limit (10% of balance)");
                }
                else
                {
                    this.Balance -= amount;
                }
            }
            else
            {
                this.Balance -= amount;
            }
        }

        public void Withdraw(double amount)
        {
            throw new NotImplementedException();
        }
    }
}

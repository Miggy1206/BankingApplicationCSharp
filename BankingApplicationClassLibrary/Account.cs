namespace BankingApplicationClassLibrary
{
    public enum AccountTypes
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
        private double interestRate;
        private DateTime interestDate;
        private DateTime maturityDate;
        private AccountTypes accountType;

        public double InterestRate
        {
            set { interestRate = value; }
            get { return interestRate; }
        }

        public AccountTypes AccountType
        {
            set { accountType = value; } 
            get { return accountType; }
        }

        public DateTime MaturityDate
        {
            set { maturityDate = value; }
            get { return maturityDate; }
        }

        public DateTime InterestDate
        {
            set { interestDate = value; }
            get { return interestDate; }
        }

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
            if(accountType == AccountTypes.FixedTerm)
            {
                throw new InvalidAccountTypeWithdrawalException("You can not withdraw of this account.");
            }

            this.Balance -= amount;
        }
    }

    public class CurrentAccount : Account, IAccount
    {
        private double overdraftLimit;
        private double overdraftInterestRate;

        public double OverdraftLimit
        {
            set { overdraftLimit = value; }
            get { return overdraftLimit; }
        }

        public double OverdraftInterestRate
        {
            set { overdraftInterestRate = value; }
            get { return overdraftInterestRate; }
        }


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
            else if (amount > this.Balance + this.OverdraftLimit)
            {
                throw new InsufficentBalanceException("Insufficient funds including overdraft limit.");
            }

            this.Balance -= amount;
        }
    }

    public class CreditAccount : Account, IAccount
    {
        private double creditLimit;
        public double creditInterestRate;
        public double withdrawalFee;
        public string accountNumber16;

        public double CreditLimit
        {
            set { creditLimit = value; }
            get { return creditLimit; }
        }

        public double CreditInterestRate
        {
            set { creditInterestRate = value; } 
            get { return creditInterestRate; }
        }

        public double WithdrawalFee
        {
            set { withdrawalFee = value; } 
            get { return withdrawalFee; }
        }

        public string AccountNumber16
        {
            set { accountNumber16 = value; } 
            get { return accountNumber16; }
        }

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
            double totalAmount = amount + (amount * this.WithdrawalFee / 100);

            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            else if (this.Balance + totalAmount > this.CreditLimit)
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
        public AccountTypes mortgageType;
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
            if (mortgageType == AccountTypes.FixedTerm)
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

using System;
using BankingApplicationClassLibrary;

namespace BankingApplication
{
    public class Program
    {
        static void Main(string[] args)
        {

            var currentAccount = new CurrentAccount
            {
                Sortcode = "12-34-56",
                AccountNumber = "00012345",
                CustomerID = "CUST001",
                Balance = 250.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF01",
                overdraftLimit = 500.00,
                overdraftInterestRate = 12.5
            };


            var savingsAccount = new SavingsAccount
            {
                Sortcode = "65-43-21",
                AccountNumber = "10054321",
                CustomerID = "CUST002",
                Balance = 1500.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF02",
                interestRate = 1.25,
                interestDate = DateTime.Now,
                maturityDate = DateTime.Now.AddYears(1),
                accountType = AccountType.InstantAccess
            };


            var creditAccount = new CreditAccount
            {
                Sortcode = "11-22-33",
                AccountNumber = "90077733",
                CustomerID = "CUST003",
                Balance = 0.00, 
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF03",
                accountNumber16 = "1234-5678-9012-3456",
                creditLimit = 2000.00,
                creditInterestRate = 18.0,
                withdrawalFee = 2.5 
            };

            var mortgageAccount = new MortgageAccount
            {
                Sortcode = "99-88-77",
                AccountNumber = "MORT123456",
                CustomerID = "CUST004",
                Balance = 250000.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF04",
                accountNumber16 = "6543-2109-8765-4321",
                totalMortgageAmount = 250000.00,
                mortgageInterestRate = 3.75,
                repaymentDate = DateTime.Now.AddYears(25),
                mortgageType = AccountType.FixedTerm,
                furtherAdvanceCharge = 150.00,
                fixedOverpaymentLimit = 1000.00
            };

            Console.WriteLine($"Current Account {currentAccount.AccountNumber} balance: {currentAccount.Balance:C}");
            Console.WriteLine($"Savings Account {savingsAccount.AccountNumber} balance: {savingsAccount.Balance:C} (Interest {savingsAccount.interestRate}%)");
            Console.WriteLine($"Credit Account {creditAccount.AccountNumber} debt: {creditAccount.Balance:C} (Limit {creditAccount.creditLimit:C})");
            Console.WriteLine($"Mortgage Account {mortgageAccount.AccountNumber} outstanding: {mortgageAccount.Balance:C} (Total mortgage {mortgageAccount.totalMortgageAmount:C}, Rate {mortgageAccount.mortgageInterestRate}%)");
        }
    }
}
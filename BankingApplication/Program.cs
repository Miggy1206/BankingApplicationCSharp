using System;
using BankingApplicationClassLibrary;

namespace BankingApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new BankingAppDbContext();
            var currentAccount = new CurrentAccount
            {
                Sortcode = "12-34-56",
                AccountNumber = "00012345",
                CustomerID = "CUST001",
                Balance = 250.00,
                OpenedByStaffID = "STAFF01",
                OverdraftLimit = 500.00,
                OverdraftInterestRate = 12.5
            };


            var savingsAccount = new SavingsAccount
            {
                Sortcode = "65-43-21",
                AccountNumber = "10054321",
                CustomerID = "CUST002",
                Balance = 1500.00,
                OpenedByStaffID = "STAFF02",
                InterestRate = 1.25,
                InterestDate = DateTime.Now,
                MaturityDate = DateTime.Now.AddYears(1),
                AccountType = AccountTypes.InstantAccess
            };


            var creditAccount = new CreditAccount
            {
                Sortcode = "11-22-33",
                AccountNumber = "90077733",
                CustomerID = "CUST003",
                Balance = 0.00, 
                OpenedByStaffID = "STAFF03",
                AccountNumber16 = "1234-5678-9012-3456",
                CreditLimit = 2000.00,
                CreditInterestRate = 18.0,
                WithdrawalFee = 2.5 
            };

            var mortgageAccount = new MortgageAccount
            {
                Sortcode = "99-88-77",
                AccountNumber = "MORT123456",
                CustomerID = "CUST004",
                Balance = 250000.00,
                OpenedByStaffID = "STAFF04",
                AccountNumber16 = "6543-2109-8765-4321",
                TotalMortgageAmount = 250000.00,
                MortgageInterestRate = 3.75,
                RepaymentDate = DateTime.Now.AddYears(25),
                MortgageType = AccountTypes.FixedTerm,
                FurtherAdvanceCharge = 150.00,
                FixedOverpaymentLimit = 1000.00
            };

            Console.WriteLine($"Current Account {currentAccount.AccountNumber} balance: {currentAccount.Balance:C}");
            Console.WriteLine($"Savings Account {savingsAccount.AccountNumber} balance: {savingsAccount.Balance:C} (Interest {savingsAccount.InterestRate}%)");
            Console.WriteLine($"Credit Account {creditAccount.AccountNumber} debt: {creditAccount.Balance:C} (Limit {creditAccount.CreditLimit:C})");
            Console.WriteLine($"Mortgage Account {mortgageAccount.AccountNumber} outstanding: {mortgageAccount.Balance:C} (Total mortgage {mortgageAccount.TotalMortgageAmount:C}, Rate {mortgageAccount.MortgageInterestRate}%)");

            dbContext.Accounts.Add(currentAccount);
            dbContext.Accounts.Add(savingsAccount);
            dbContext.Accounts.Add(creditAccount);
            dbContext.Accounts.Add(mortgageAccount);

            dbContext.SaveChanges();

            var allAccounts = dbContext.Accounts.ToList();


            foreach (var account in allAccounts)
            {
                switch (account)
                {
                    case CurrentAccount current:
                        Console.WriteLine($"Current Account: {current.accountID}, Overdraft: {current.OverdraftLimit}");
                        break;
                    case SavingsAccount savings:
                        Console.WriteLine($"Savings Account: {savings.accountID}, Interest: {savings.InterestRate}");
                        break;
                    case MortgageAccount mortgage:
                        Console.WriteLine($"Mortgage: {mortgage.accountID}, Loan: {mortgage.TotalMortgageAmount}");
                        break;
                    case CreditAccount credit:
                        Console.WriteLine($"Credit Card: {credit.accountID}, Limit: {credit.CreditLimit}");
                        break;
                }
            }

        }
    }
}
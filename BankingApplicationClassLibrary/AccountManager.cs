using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationClassLibrary
{
    
    public class AccountManager
    {

        private readonly BankingAppDbContext dbContext;

        public AccountManager(BankingAppDbContext context)
        {
            dbContext = context;
        }

        public void ViewUserAccounts(string userID)
        {
            var accounts = dbContext.Accounts.Where(a => a.CustomerID == userID).ToList();
            foreach (var account in accounts)
            {
                switch(account)
                {
                    case CurrentAccount currentAccount:
                        Console.WriteLine($"[Current Account], Sortcode: {currentAccount.Sortcode}, Account Number: {currentAccount.AccountNumber}, Balance: {currentAccount.Balance}, Overdraft Limit: {currentAccount.OverdraftLimit}, Overdraft Interest Rate: {currentAccount.OverdraftInterestRate}");
                        break;
                    case SavingsAccount savingsAccount:
                        Console.WriteLine($"[Savings Account], Sortcode: {savingsAccount.Sortcode}, Account Number: {savingsAccount.AccountNumber}, Balance: {savingsAccount.Balance}, Interest Rate: {savingsAccount.InterestRate}");
                        break;
                    case CreditAccount creditCard:
                        Console.WriteLine($"[Credit Card], Sortcode: {creditCard.Sortcode}, Account Number: {creditCard.AccountNumber}, Balance: {creditCard.Balance}, Credit Limit: {creditCard.CreditLimit}, Credit Interest Rate: {creditCard.CreditInterestRate}, Withdrawal Fee: {creditCard.WithdrawalFee}, Account Number (16 digits): {creditCard.AccountNumber16}");
                        break;
                    case MortgageAccount mortgage:
                        Console.WriteLine($"[Mortgage] Account ID:, Sortcode: {mortgage.Sortcode}, Account Number: {mortgage.AccountNumber}, Balance: {mortgage.Balance}, Total Mortgage Amount: {mortgage.TotalMortgageAmount}, Mortgage Interest Rate: {mortgage.MortgageInterestRate}");
                        break;
                    default:
                        Console.WriteLine($"[Unknown Account Type], Sortcode: {account.Sortcode}, Account Number: {account.AccountNumber}, Balance: {account.Balance}");
                        break;
                }
            }
        }

        public void CreateCurrentAccount(string sortcode, string userID, double overdraftLimit,double overdraftInterestRate)
        {
            var account = new CurrentAccount
            {
                AccountID = String.Empty,
                Sortcode = sortcode,
                AccountNumber = String.Empty,
                CustomerID = userID,
                Balance = 0,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "SYSTEM",
                OverdraftLimit = overdraftLimit,
                OverdraftInterestRate = overdraftInterestRate
            };
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
        }


    }
}

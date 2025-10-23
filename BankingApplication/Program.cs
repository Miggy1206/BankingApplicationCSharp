using System;
using BankingApplicationClassLibrary;

namespace BankingApplication
{
    public class Program
    {

        private BankingAppDbContext dbContext = new BankingAppDbContext();

        static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true); // Intercept to prevent displaying the key
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Mask the input with '*'
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1]; // Remove the last character
                    Console.Write("\b \b"); // Erase the '*' from the console
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }






        static void StaffView()
        {
            Console.WriteLine("Staff View - Options will be implemented here.");
        }


        static void CustomerView(Customer customer)
        {
            using var dbContext = new BankingAppDbContext();
            AccountManager accountManager = new AccountManager(dbContext);
            Console.WriteLine("Customer View - Options will be implemented here.");
            bool exit = false;
            while (!exit)
            {
                Console.Write("Choose on of the options:\n[1] View Accounts\n[2] Open a New Account\n[3] Close an account\n[4] Logout\n[]");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("View Accounts");
                        accountManager.ViewUserAccounts(customer.UserID);
                        break;
                    case "2":
                        bool back = false;
                        while (!back)
                        {
                            Console.WriteLine("Open a New Account");
                            Console.Write("Choose an account type:\n[1] Current Account\n[2] Savings Account\n[3] Credit Card\n[4] Mortgage\n[5] Back\n[]");
                            string accountTypeChoice = Console.ReadLine();
                            switch (accountTypeChoice)
                            {
                                case "1":
                                    Console.WriteLine("Opening Current Account...");
                                    string sortcode = "80-10-11";
                                    Console.WriteLine("Your home branch is 80-10-11");

                                    double overdraftAmount = 0;
                                    double interestRate = 15.0;

      
                                    Console.WriteLine("How much overdraft would you like to apply for? The current interest rate is 15%. (Default £0)");
                                    overdraftAmount = Convert.ToDouble(Console.ReadLine());


                                    accountManager.CreateCurrentAccount(
                                        sortcode: sortcode,
                                        userID: customer.UserID,
                                        overdraftLimit: overdraftAmount,
                                        overdraftInterestRate: interestRate
                                        );

                                    Console.WriteLine("Current Account opened successfully!");
                                    break;
                                case "2":
                                    Console.WriteLine("Opening Savings Account...");
                                    // Implement Savings Account opening logic here
                                    break;
                                case "3":
                                    Console.WriteLine("Opening Credit Card...");
                                    // Implement Credit Card opening logic here
                                    break;
                                case "4":
                                    Console.WriteLine("Opening Mortgage...");
                                    // Implement Mortgage opening logic here
                                    break;
                                case "5":
                                    back = true;
                                    break;
                                default:
                                    Console.WriteLine("Invalid option. Please try again.");
                                    break;
                            }
                        }
                        break;
                    case "3":
                        Console.WriteLine("Close an account");
                        break;
                    case "4":
                        Console.WriteLine("Logging out...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }


            }
        }




        static void Main(string[] args)
        {

            using var dbContext = new BankingAppDbContext();


            //var userManager = new UserManager(dbContext);
            //userManager.CreateStaff(
            //    firstName: "Miguel",
            //    lastName: "Barbosa",
            //    email: "staff@gmail.com",
            //    dateOfBirth: new DateTime(1990, 5, 15),
            //    plainPassword: "SecurePassword123!"
            //    );

            Console.WriteLine("BANKING APPLICATION");
            Console.WriteLine("===================");
            Console.WriteLine("Loading...");
            Console.WriteLine("Login");
            Console.WriteLine("===================");
            Console.Write("[Email]: ");
            string email = Console.ReadLine();
            Console.Write("[Password]: ");
            string password = ReadPassword();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Email and Password cannot be empty.");
                return;
                
            }

            AuthService authService = new AuthService(dbContext);
            User user = authService.Login(email, password);
            if (user != null)
            {
                Console.WriteLine($"\nWelcome, {user.FirstName} {user.LastName}!");
            }
            else
            {
                Console.WriteLine("\nLogin failed. Please check your credentials.");
                return;
            }

            switch(user)
            {
                case Staff staff:
                    StaffView();
                    break;
                case Customer customer:
                    CustomerView(customer);
                    break;
                default:
                    Console.WriteLine("Unknown user type.");
                    break;
            }


























            var currentAccount = new CurrentAccount
            {
                AccountID = string.Empty,
                Sortcode = "12-34-56",
                AccountNumber = string.Empty,
                CustomerID = "CUST001",
                Balance = 250.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF01",
                OverdraftLimit = 500.00,
                OverdraftInterestRate = 12.5
            };


            var savingsAccount = new SavingsAccount
            {
                AccountID = "",
                Sortcode = "65-43-21",
                AccountNumber = "10054321",
                CustomerID = "CUST002",
                Balance = 1500.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF02",
                InterestRate = 1.25,
                InterestDate = DateTime.Now,
                MaturityDate = DateTime.Now.AddYears(1),
                AccountType = AccountTypes.InstantAccess
            };


            var creditAccount = new CreditAccount
            {
                AccountID = "",
                Sortcode = "11-22-33",
                AccountNumber = "90077733",
                CustomerID = "CUST003",
                Balance = 0.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF03",
                AccountNumber16 = "1234-5678-9012-3456",
                CreditLimit = 2000.00,
                CreditInterestRate = 18.0,
                WithdrawalFee = 2.5
            };

            var mortgageAccount = new MortgageAccount
            {
                AccountID = "",
                Sortcode = "99-88-77",
                AccountNumber = "MORT123456",
                CustomerID = "CUST004",
                Balance = 250000.00,
                OpenedDate = DateTime.Now,
                OpenedByStaffID = "STAFF04",
                AccountNumber16 = "6543-2109-8765-4321",
                TotalMortgageAmount = 250000.00,
                MortgageInterestRate = 3.75,
                RepaymentDate = DateTime.Now.AddYears(25),
                MortgageType = AccountTypes.FixedTerm,
                FurtherAdvanceCharge = 150.00,
                FixedOverpaymentLimit = 10.00,
                MortgageTermInYears = 25
            };

            savingsAccount.CalculateInterest();


            

            //dbContext.Users.Add(user);

            //dbContext.Accounts.Add(currentAccount);
            //dbContext.Accounts.Add(savingsAccount);
            //dbContext.Accounts.Add(creditAccount);
            //dbContext.Accounts.Add(mortgageAccount);

            //dbContext.SaveChanges();


            //var allAccounts = dbContext.Accounts.ToList();


            //foreach (var account in allAccounts)
            //{
            //    switch (account)
            //    {
            //        case CurrentAccount current:
            //            Console.WriteLine($"Current Account: {current.AccountNumber}, Overdraft: {current.OverdraftLimit}");
            //            break;
            //        case SavingsAccount savings:
            //            Console.WriteLine($"Savings Account: {savings.AccountID}, Interest: {savings.InterestRate}");
            //            break;
            //        case MortgageAccount mortgage:
            //            Console.WriteLine($"Mortgage: {mortgage.AccountID}, Loan: {mortgage.TotalMortgageAmount}");
            //            break;
            //        case CreditAccount credit:
            //            Console.WriteLine($"Credit Card: {credit.AccountID}, Limit: {credit.CreditLimit}");
            //            break;
            //    }
            //}

        }

    }
   
}
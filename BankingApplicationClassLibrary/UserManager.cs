using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationClassLibrary
{


    public class UserManager
    {

        private readonly BankingAppDbContext dbContext;
        private readonly AuthService authService;

        public UserManager(BankingAppDbContext context)
        {
            dbContext = context;
            authService = new AuthService(context);
        }

        public Customer CreateCustomer(string firstName, string lastName, string email, DateTime dateOfBirth, string plainPassword)
        {
            var hashedPassword = authService.HashPassword(plainPassword);
            var customer = new Customer
            {
                UserID = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = dateOfBirth,
                WithBankSince = DateTime.Now,
                Password = hashedPassword
            };
            dbContext.Users.Add(customer);
            dbContext.SaveChanges();
            return customer;
        }

        public Staff CreateStaff(string firstName, string lastName, string email, DateTime dateOfBirth, string plainPassword)
        {
            var hashedPassword = authService.HashPassword(plainPassword);
            var staff = new Staff
            {
                UserID = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = dateOfBirth,
                WithBankSince = DateTime.Now,
                Password = hashedPassword
            };
            dbContext.Users.Add(staff);
            dbContext.SaveChanges();

            return staff;
        }

        public void DeleteUser(string userID)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.UserID == userID);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationClassLibrary
{
    public class AuthService
    {
        private readonly BankingAppDbContext dbContext;

        public AuthService(BankingAppDbContext context)
        {
            dbContext = context;
        }

        //public User Login(string email, string password)
        //{
        //    var user = dbContext.Users.SingleOrDefault(u => u.Email == email);
        //    if (user == null)
        //        throw new UnauthorizedAccessException("User not found.");

        //    // Compare password (hash in production!)
        //    if (user.Password != password)
        //        throw new UnauthorizedAccessException("Invalid password.");

        //    return user; // successful login
        //}

    }
}

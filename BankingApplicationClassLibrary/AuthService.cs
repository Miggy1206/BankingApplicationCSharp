using Microsoft.AspNetCore.Identity;
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
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        public AuthService(BankingAppDbContext context)
        {
            dbContext = context;
        }

        public User Login(string email, string password)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            if (!VerifyPassword(user.Password, password))
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            Console.WriteLine($"User {user.FirstName} {user.LastName} logged in successfully.");

            return user; 
        }

        public string HashPassword(string plainPassword)
        {
            return _passwordHasher.HashPassword(null, plainPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }




    }
}

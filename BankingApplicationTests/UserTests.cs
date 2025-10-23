using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApplicationClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace BankingApplicationTests
{
    public class UserTests
    {

        //AuthService Login Test
        [Trait("Category", "AuthService")]
        [Fact]
        public void AuthService_Login_Valid_Credentials_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BankingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "master")
                .Options;
            using (var context = new BankingAppDbContext())
            {
                var userManager = new UserManager(context);
                var authService = new AuthService(context);
                Staff user = userManager.CreateStaff("Test", "User", "test@gmail.com", new DateTime(1990, 1, 1), "Password123");

                
                var loggedInUser = authService.Login("test@gmail.com", "Password123");
              
                Assert.NotNull(loggedInUser);
                Assert.Equal(user.UserID, loggedInUser.UserID);
                
                userManager.DeleteUser(loggedInUser.UserID);


            }



        }

        [Trait("Category", "AuthService")]
        [Fact]
        public void AuthService_Login_Invalid_Password_Throws_UnauthorizedAccessException()
        {
            var options = new DbContextOptionsBuilder<BankingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "master")
                .Options;
            using (var context = new BankingAppDbContext())
            {
                var userManager = new UserManager(context);
                var authService = new AuthService(context);
                Staff staff = userManager.CreateStaff("Test", "User", "test@gmail.com", new DateTime(1990, 1, 1), "Password123");
                // Act & Assert
                var exception = Assert.Throws<UnauthorizedAccessException>(() => authService.Login("test@gmail.com", "WrongPassword"));
                Assert.Equal("Invalid password.", exception.Message);
                userManager.DeleteUser(staff.UserID);
            }
        }

            [Trait("Category", "AuthService")]
        [Fact]
        public void AuthService_Login_Nonexistent_User_Throws_UnauthorizedAccessException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BankingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "master")
                .Options;
            using (var context = new BankingAppDbContext())
            {
                var authService = new AuthService(context);
                // Act & Assert
                Assert.Throws<UnauthorizedAccessException>(() => authService.Login("NoUser@gmail.com", "AnyPassword"));
            }
        }
    }
}

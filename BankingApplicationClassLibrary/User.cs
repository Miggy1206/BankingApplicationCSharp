using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationClassLibrary
{
    public enum Permissions
    {
        None = 0,
        ViewAccounts = 1,
        Deposit = 2,
        Withdraw = 4,
        Transfer = 8,
        CreateAccount = 16,
        CloseAccount = 32,
        ManageUsers = 64,
        CustomerPermissions = ViewAccounts | Deposit | Withdraw | Transfer,
        StaffPermissions = ViewAccounts | Deposit | Withdraw | Transfer | CreateAccount | CloseAccount | ManageUsers
    }


    public class User
    {
        private string userID;
        private string email;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private DateTime withBankSince;
        private string password;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public DateTime WithBankSince
        {
            get { return withBankSince; }
            set { withBankSince = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }




    }


    public class Customer : User
    {

    }


    public class Staff : User
    {

    }
}

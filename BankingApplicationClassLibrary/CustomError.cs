using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationClassLibrary
{
    public class InsufficentBalanceException : Exception
    {
        public InsufficentBalanceException()
        {
        }
        public InsufficentBalanceException(string message) : base(message)
        {
        }
        public InsufficentBalanceException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class InvalidAmountException : Exception
    {
        public InvalidAmountException()
        {
        }
        public InvalidAmountException(string message) : base(message)
        {
        }
        public InvalidAmountException(string message, Exception inner) : base(message, inner)
        {
        }

    }


}

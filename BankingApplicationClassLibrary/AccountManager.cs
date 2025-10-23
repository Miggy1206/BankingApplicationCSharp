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


    }
}

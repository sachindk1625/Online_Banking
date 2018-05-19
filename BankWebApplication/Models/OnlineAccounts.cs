using System;
using System.Collections.Generic;

namespace BankWebApplication.Models
{
    public partial class OnlineAccounts
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public int AccountNumber { get; set; }

        public AccountDetails AccountNumberNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BankWebApplication.Models
{
    public partial class AllTransactions
    {
        public int TransId { get; set; }
        public int AccountNumber { get; set; }
        public string Statements { get; set; }
        public DateTime? Date { get; set; }

        public AccountDetails AccountNumberNavigation { get; set; }
    }
}

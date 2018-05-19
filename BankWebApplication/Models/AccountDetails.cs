using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BankWebApplication.Models
{
    public partial class AccountDetails
    {
        public AccountDetails()
        {
            
            AllTransactions = new HashSet<AllTransactions>();
        }
        
        public int AccountNumber { get; set; }
        [Required(ErrorMessage ="Please enter the First name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the Last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the Address.")]
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public byte[] Proof { get; set; }
        public string Ssn { get; set; }

        public ICollection<AllTransactions> AllTransactions { get; set; }
    }
}

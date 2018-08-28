using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDTO
{
    public class AccountDTO
    {
        public System.Guid Customer_Id { get; set; }
        public int SavingId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string RoutingNumber { get; set; }

        //public virtual tbCustomer tbCustomer { get; set; }
    }
}

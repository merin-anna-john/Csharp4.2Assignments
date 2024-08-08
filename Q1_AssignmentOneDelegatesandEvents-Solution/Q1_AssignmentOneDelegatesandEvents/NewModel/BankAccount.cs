using Q1_AssignmentOneDelegatesandEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1_AssignmentOneDelegatesandEvents.NewModel
{
    public class BankAccount : IBankAccount
    {
        public int AccountNumber { get; set; }
        public double Balance { get; set; }
        public string OwnerName { get; set; }

        public BankAccount(int accountNumber, string ownerName)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = 0;
        }
    }
}

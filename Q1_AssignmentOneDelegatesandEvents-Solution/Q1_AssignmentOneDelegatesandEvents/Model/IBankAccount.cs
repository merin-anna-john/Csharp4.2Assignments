using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1_AssignmentOneDelegatesandEvents.Model
{
    public interface IBankAccount
    {
        int AccountNumber { get; set; }
        double Balance { get; set; }
        string OwnerName { get; set; }
    }
}

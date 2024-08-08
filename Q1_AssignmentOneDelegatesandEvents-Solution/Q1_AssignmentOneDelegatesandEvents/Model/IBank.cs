using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1_AssignmentOneDelegatesandEvents.Model
{
    public delegate void TransactionDelegate(int accountNumber, double amount);

    public interface IBank
    {
        event TransactionDelegate OnDeposit;
        event TransactionDelegate OnWithdraw;
        event Action<int> OnCheckBalance;

        void AddNewAccount(string name);
        void Deposit(int accountNumber, double amount);
        void Withdraw(int accountNumber, double amount);
        void CheckBalance(int accountNumber);
    }
}

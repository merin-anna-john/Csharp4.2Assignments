using System;
using Q1_AssignmentOneDelegatesandEvents.Model;
using Q1_AssignmentOneDelegatesandEvents.NewModel;

namespace Q1_AssignmentOneDelegatesandEvents
{
    public class Program
    {
        public delegate void AddAccountDelegate(string name);
        public delegate void DepositDelegate(int accountNumber, double amount);
        public delegate void WithdrawDelegate(int accountNumber, double amount);
        public delegate void BalanceDelegate(int accountNumber);

        static void Main(string[] args)
        {
            IBank bank = new Bank();
            AddAccountDelegate addAccountDelegate = new AddAccountDelegate(bank.AddNewAccount);
            DepositDelegate depositDelegate = new DepositDelegate(bank.Deposit);
            WithdrawDelegate withdrawDelegate = new WithdrawDelegate(bank.Withdraw);
            BalanceDelegate balanceDelegate = new BalanceDelegate(bank.CheckBalance);

            while (true)
            {
                Console.WriteLine("MENU");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Check Balance");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter your choice:");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter account holder name:");
                        string name = Console.ReadLine();
                        bank.AddNewAccount(name);
                        break;
                    case 2:
                        Console.WriteLine("Enter the account number:");
                        int depositAccountNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the amount to deposit:");
                        double depositAmount = double.Parse(Console.ReadLine());
                        bank.Deposit(depositAccountNumber, depositAmount);
                        break;
                    case 3:
                        Console.WriteLine("Enter the account number:");
                        int withdrawAccountNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the amount to withdraw:");
                        double withdrawAmount = double.Parse(Console.ReadLine());
                        bank.Withdraw(withdrawAccountNumber, withdrawAmount);
                        break;
                    case 4:
                        Console.WriteLine("Enter the account number:");
                        int balanceAccountNumber = int.Parse(Console.ReadLine());
                        bank.CheckBalance(balanceAccountNumber);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Wrong Choice");
                        break;
                }
            }
        }
    }
}

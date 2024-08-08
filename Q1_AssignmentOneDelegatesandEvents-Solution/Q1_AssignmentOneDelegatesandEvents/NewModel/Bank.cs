using Q1_AssignmentOneDelegatesandEvents.NewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Q1_AssignmentOneDelegatesandEvents.Model
{
    public class Bank : IBank
    {
        private Dictionary<int, BankAccount> accounts = new Dictionary<int, BankAccount>();
        private int nextAccountNumber = 123456789; // Starting account number

        public event TransactionDelegate OnDeposit;
        public event TransactionDelegate OnWithdraw;
        public event Action<int> OnCheckBalance;

        public void AddNewAccount(string name)
        {
            if (IsValidName(name))
            {
                int accountNumber = GenerateAccountNumber();
                accounts[accountNumber] = new BankAccount(accountNumber, name);
                Console.WriteLine($"Account holder name is {name}\nAccount number is {accountNumber}.");
            }
            else
            {
                Console.WriteLine($"{name} is not a valid name.");
            }
        }

        public static bool IsValidName(string name)
        {
            // Regex to match only letters and spaces
            string pattern = @"^[a-zA-Z\s]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(name);
        }

        public int GenerateAccountNumber()
        {
            while (accounts.ContainsKey(nextAccountNumber))
            {
                nextAccountNumber++;
            }
            return nextAccountNumber++;
        }

        public void Deposit(int accountNumber, double amount)
        {
            try
            {
                if (accounts.ContainsKey(accountNumber))
                {
                    accounts[accountNumber].Balance += amount;
                    Console.WriteLine($"Deposited {amount} to account {accountNumber}. New balance: {accounts[accountNumber].Balance}");
                    OnDeposit?.Invoke(accountNumber, amount);
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Withdraw(int accountNumber, double amount)
        {
            try
            {
                if (accounts.ContainsKey(accountNumber))
                {
                    if (accounts[accountNumber].Balance >= amount)
                    {
                        accounts[accountNumber].Balance -= amount;
                        Console.WriteLine($"Withdrew {amount} from account {accountNumber}. New balance: {accounts[accountNumber].Balance}");
                        OnWithdraw?.Invoke(accountNumber, amount);
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CheckBalance(int accountNumber)
        {
            try
            {
                if (accounts.ContainsKey(accountNumber))
                {
                    Console.WriteLine($"Account {accountNumber} balance: {accounts[accountNumber].Balance}");
                    OnCheckBalance?.Invoke(accountNumber);
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

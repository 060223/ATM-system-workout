
using System;
using System.Collections.Generic;

namespace ATMSimulation.Models
{
    public class Account
    {
        public string CardNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public decimal Balance { get; set; }
        public bool IsLocked { get; set; }
        public int FailedAttempts { get; set; }
        public string Location { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void AddTransaction(string type, decimal amount, string targetAccount = null, string description = null)
        {
            Transactions.Add(new Transaction
            {
                Time = DateTime.Now,
                Type = type,
                Amount = amount,
                TargetAccount = targetAccount,
                Description = description ?? $"{type} {amount}元"
            });
        }
    }
}

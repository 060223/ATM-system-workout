using System;

namespace ATMSimulation.Models
{
    public class Transaction
    {
        public DateTime Time { get; set; }
        public string Type { get; set; } // "Withdraw", "Deposit", "Transfer"
        public decimal Amount { get; set; }
        public string TargetAccount { get; set; }
        public string Description { get; set; }
    }
}
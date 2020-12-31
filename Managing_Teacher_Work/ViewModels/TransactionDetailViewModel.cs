using Managing_Teacher_Work.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.ViewModels
{
    public class TransactionDetailViewModel
    {
        public string Month { get; set; }
        public decimal Amount { get; set; }

        public TransactionDetailViewModel(string month, decimal amount)
        {
            Month = month;
            Amount = amount;
        }
    }
}
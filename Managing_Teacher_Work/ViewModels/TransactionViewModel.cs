using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.ViewModels
{
    public class TransactionViewModel
    {
        public int ID { get; set; }
        public bool IsPaid { get; set; }
        public decimal Amout { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TeacherName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Models
{
    [Table("Transaction")]
    public class Transaction : BaseEntity
    {
        public int TeacherId { get; set; }
        public bool IsPaid { get; set; }
        public decimal Amout { get; set; }
        public int TransactionType { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
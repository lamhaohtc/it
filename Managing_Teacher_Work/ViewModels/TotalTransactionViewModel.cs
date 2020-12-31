using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Managing_Teacher_Work.ViewModels
{
    [DataContract]
    public class TotalTransactionViewModel
    {
        public decimal TotalUnionFee { get; set; }
        public decimal TotalCharityFee { get; set; }
        public decimal TotalDonateFee { get; set; }
    }
}
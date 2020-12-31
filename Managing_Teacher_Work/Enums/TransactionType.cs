using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Enums
{
    public enum TransactionType
    {
        [Description("Công Đoàn Phí")]
        UNION_FEE = 1,
        [Description("Quyên Góp")]
        DONATE,
        [Description("Quỹ Từ Thiện")]
        CHARITY_FEE
    }
}
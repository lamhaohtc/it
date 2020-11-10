using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Enums
{
    public enum ActivityStatus
    {
        [Description("Sắp Diễn Ra")]
        Ready = 1,
        [Description("Đang Diễn Ra")]
        OnGoing,
        [Description("Đã Hoàn Thành")]
        Done
    }
}
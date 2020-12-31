using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Enums
{
    public enum UserStatus
    {
        [Description("Chưa Kích Hoạt")]
        Free, 
        [Description("Đã Kích Hoạt")]
        Activated
    }
}
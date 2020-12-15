using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Enums
{
    public enum RoleEnum
    {
        [Description("Chủ Tịch")]
        CT,
        [Description("Phó Chủ Tịch")]
        PCT,
        [Description("Ủy Viên Ban Chấp Hành")]
        UV,
        [Description("Thành Viên")]
        DV,
        [Description("Tổ Trưởng")]
        TT,
        [Description("Tổ Phó")]
        TP,
        [Description("Ủy Viên Ban Thường Vụ")]
        UV_BTV
    }
}
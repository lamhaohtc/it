using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Enums
{
    public enum ActivityTypeEnum
    {
        [Description("Văn Nghệ")]
        Art = 1,
        [Description("Thể Dục - Thể Thao")]
        Sport,
        [Description("Từ Thiện")]
        Charity,
        [Description("Tình Nguyện")]
        Volunteer
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Managing_Teacher_Work.Models
{

    [Table("CalendarWorking")]
    public partial class CalendarWorking : BaseEntity
    {

        [StringLength(255)]
        public string Name_CalendarWorking { get; set; }

        public string Description { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public int TeacherID { get; set; }

        public int WorkID { get; set; }

        public int TypeCalendarID { get; set; }

        public int WorkState { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Files { get; set; }

        public virtual TypeCalendar TypeCalendar { get; set; }

        public virtual Work Work { get; set; }
    }
}

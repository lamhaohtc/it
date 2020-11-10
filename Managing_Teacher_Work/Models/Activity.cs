using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Models
{
    public class Activity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ActivityType { get; set; }
        public string Address { get; set; }
        public int ActivityStatus { get; set; }
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Models
{
    public class TeacherActivity : BaseEntity
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
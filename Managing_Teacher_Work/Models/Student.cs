using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Managing_Teacher_Work.Models
{

    [Table("Student")]
    public partial class Student : BaseEntity
    {

        [StringLength(255)]
        public string Name_Student { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public int ClassID { get; set; }

        public virtual Class Class { get; set; }
    }
}

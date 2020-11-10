using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Managing_Teacher_Work.Models
{

    [Table("Teacher")]
    public partial class Teacher : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            Class = new HashSet<Class>();
            Transactions = new HashSet<Transaction>();
            TeacherActivities = new HashSet<TeacherActivity>();
        }


        [StringLength(255)]
        public string Name_Teacher { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        public bool? IsDelete { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public int SicenceID { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Class { get; set; }

        public virtual Science Science { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
    }
}

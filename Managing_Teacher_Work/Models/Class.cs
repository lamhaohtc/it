using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Managing_Teacher_Work.Models
{

    [Table("Class")]
    public partial class Class : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Student = new HashSet<Student>();
        }


        [StringLength(255)]
        public string ClassCode { get; set; }

        [StringLength(255)]
        public string ClassName { get; set; }

        public int TeacherID { get; set; }

        public int ScienceID { get; set; }

        public virtual Science Science { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Student { get; set; }
    }
}

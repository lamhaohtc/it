using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Managing_Teacher_Work.Models
{

    [Table("User")]
    public partial class User : BaseEntity
    {

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(50)]
        public string GroupID { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

    }
}

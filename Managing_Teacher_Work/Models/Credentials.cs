using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Managing_Teacher_Work.Models
{
    public partial class Credentials
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string UserGroupID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RoleID { get; set; }
    }
}

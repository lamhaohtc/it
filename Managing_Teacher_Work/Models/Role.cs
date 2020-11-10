using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Managing_Teacher_Work.Models
{

    [Table("Role")]
    public partial class Role
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
    }
}

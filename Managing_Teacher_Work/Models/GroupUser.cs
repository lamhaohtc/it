using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Managing_Teacher_Work.Models
{

    [Table("GroupUser")]
    public partial class GroupUser
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(255)]
        public string Name_GroupUser { get; set; }

        [StringLength(10)]
        public string CodeRole { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Managing_Teacher_Work.Models
{

    [Table("Menu")]
    public partial class Menu : BaseEntity
    {

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string MenuUrl { get; set; }

        [StringLength(255)]
        public string MenuICon { get; set; }

        public bool? Enable { get; set; }

    }
}

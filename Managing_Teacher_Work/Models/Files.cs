using System.ComponentModel.DataAnnotations;


namespace Managing_Teacher_Work.Models
{

    public partial class Files
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public string FileForm { get; set; }
    }
}

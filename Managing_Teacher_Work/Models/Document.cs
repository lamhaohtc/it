using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Managing_Teacher_Work.Models
{
    [Table("Document")]
    public partial class Document : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }

    }
}

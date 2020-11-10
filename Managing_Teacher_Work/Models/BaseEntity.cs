using System;
using System.ComponentModel.DataAnnotations;

namespace Managing_Teacher_Work.Models
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
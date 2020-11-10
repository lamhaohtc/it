using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.ViewModels
{
    public class TeacherViewModel
    {
        public int ID { get; set; }
        public string Name_Teacher { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }   

        public string Gender { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
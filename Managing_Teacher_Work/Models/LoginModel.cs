﻿using System.ComponentModel.DataAnnotations;


namespace Managing_Teacher_Work.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời bạn nhập UserName")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời bạn nhập Password")]
        public string PassWord { set; get; }
        public bool remember { set; get; }
    }
}
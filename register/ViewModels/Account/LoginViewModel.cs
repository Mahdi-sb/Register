using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace register.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید"), Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [Display(Name ="رمز عبور")]
        [DataType(DataType.Password )]
        public string Password { get; set; }



    }
}

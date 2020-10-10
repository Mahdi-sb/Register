using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace register.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required , Display(Name ="UserName")]
        public string UserName { get; set; }

        [Required , Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}

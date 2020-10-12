using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace register.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام را وارد کنید")]
        [Display(Name ="نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage ="ایمیل معتبر نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="رمز عبور حداقل باید 6 حرف باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [Display(Name = "تایید رمز عبور")]
        [Compare(nameof(Password),ErrorMessage ="تکرار رمزعبور با رمز عبور مغایرت دارد")]
        [DataType(DataType.Password)]
        public string ConfPassword { get; set; }




    }
}

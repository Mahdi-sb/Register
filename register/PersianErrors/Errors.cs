using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace register.PersianErrors
{
    public class Errors:IdentityErrorDescriber//////بازنویسی متد های مربوط به ارور برای فارسی کردن ارور ها
    {
        ////بازنویسی متد ایمیل موجود در دیتابیس
        public override IdentityError DuplicateEmail(string email) => new IdentityError()
        {
            
            Code = nameof(DuplicateEmail),
            Description =$"ایمیل'{email}'قبلا توسط شخص دیگری انتخاب شده است"
        };

        ////بازنویسی متد نام کاربری موجود در دیتابیس
        public override IdentityError DuplicateUserName(string userName) => new IdentityError()

        {
            Code = nameof(DuplicateUserName),
            Description = $"نام کاربری'{userName}'قبلا توسط شخص دیگری انتخاب شده است"
        };

        ////بازنویسی متد ایمیل غیرمجاز در دیتابیس
        public override IdentityError InvalidEmail(string email) => new IdentityError()
        {
            Code = nameof(InvalidEmail),
            Description = $"ایمیل'{email}'معتبر نیست"
        };

        ////بازنویسی متد نقش موجود در دیتابیس
        public override IdentityError DuplicateRoleName(string role) => new IdentityError()
        {
            Code = nameof(DuplicateRoleName),
            Description = $"مقام'{role}'قبلا ثبت شده"
        };

    }
}

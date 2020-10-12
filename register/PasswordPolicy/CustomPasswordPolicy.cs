using Microsoft.AspNetCore.Identity;
using register.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace register.Password
{
    public class CustomPasswordPolicy : PasswordValidator<AppUser>////شخصی سازی کردن رمز عبور 
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();
            //if password contains a space 
            if (password.ToLower().Contains(" "))
            {
                errors.Add(new IdentityError
                {
                    Description = "رمز عبور نباید شامل فاصله باشد"
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

    }
}

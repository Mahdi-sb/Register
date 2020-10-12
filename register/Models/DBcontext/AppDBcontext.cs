using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace register.Models.DBcontext
{
    public class AppDBcontext : IdentityDbContext<AppUser>
    {
        public AppDBcontext(DbContextOptions dbContextOptions)

            : base(dbContextOptions)
        {

        }


    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using register.Models.Questions;
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

        public DbSet<ModelQuestion> Questions { get; set; }
        public DbSet<ModelAnswer> Answers { get; set; }
        
    }
}

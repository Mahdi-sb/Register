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
        //public AppDBcontext() { }
        public AppDBcontext(DbContextOptions dbContextOptions)

            : base(dbContextOptions)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//connection
        //{
        //    optionsBuilder.UseSqlServer("server=localhost;Database=EmploeeDB;Trusted_connection=True;MultipleActiveResultSets=true ");
        //    base.OnConfiguring(optionsBuilder);
        //}
        //public DbSet<AppUser>  appUsers { get; set; }
    }
}

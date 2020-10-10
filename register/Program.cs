using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
//using ContosoUniversity.Data;

namespace register
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //AppDBcontext db = new AppDBcontext();
            //db.Database.EnsureCreated();
            
            
           

           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

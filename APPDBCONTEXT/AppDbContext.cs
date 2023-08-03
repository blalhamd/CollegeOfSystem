using CollegeOfSystem.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CollegeOfSystem.APPDBCONTEXT
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Doctor> doctors { get; set; }


       public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
       {
       }

       public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }




    }
}

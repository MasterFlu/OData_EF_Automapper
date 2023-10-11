using Example.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Timesheet = Example.Entities.Models.TimeSheet;

namespace Example.Entities.Database
{
   public class ExampleDbContext : DbContext
   {
      protected readonly IConfiguration Configuration;

      public virtual DbSet<Process> Processes { get; set; }
      public virtual DbSet<Timesheet> TimeSheets { get; set; }

      public ExampleDbContext(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      protected override void OnConfiguring(DbContextOptionsBuilder options)
      {
         // connect to sqlite database
         options.UseSqlite(Configuration.GetConnectionString("DemoDatabase"));
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
      }
   }
}

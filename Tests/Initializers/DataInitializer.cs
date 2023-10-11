using Example.Entities.Database;
using Example.Entities.Models;
using Example.Tests.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Tests.Initializers
{
   public class DataInitializer : IDisposable
   {
      private readonly TestApplication<Program> _factory;

      public DataInitializer(TestApplication<Program> factory) 
      { 
         _factory = factory;
      }

      public void Dispose()
      {
         TruncateTables();
      }

      public void CreateData()
      {
         using (var scope = _factory.Services.CreateScope())
         {
            var random = new Random();
            var dbContext = scope.ServiceProvider.GetService<ExampleDbContext>()!;

            for (int i = 0; i < 10; i++)
            {
               Process process = new Process();
               process.Name = "Process " + i.ToString();

               dbContext.Processes.Add(process);
               dbContext.SaveChanges();

               //Create TimeSheet for every process
               for (int j = 0; j < 2; j++)
               {
                  var timeSheet = new TimeSheet();
                  timeSheet.Name = "Timesheet";
                  timeSheet.StartDateTime = DateTime.Now.AddMinutes(random.Next(0, 50));
                  timeSheet.EndDateTime = timeSheet.StartDateTime.AddMinutes(30);
                  timeSheet.Process = process;

                  dbContext.TimeSheets.Add(timeSheet);
                  dbContext.SaveChanges();
               }
            }
         }
      }

      private void TruncateTables()
      {
         using (var scope = _factory.Services.CreateScope())
         {
            var dbContext = scope.ServiceProvider.GetService<ExampleDbContext>()!;

            var tableNames = dbContext.Model.GetEntityTypes()
             .Select(t => t.GetTableName())
             .Distinct()
             .ToList();

            dbContext.Database.ExecuteSqlRaw("PRAGMA foreign_keys=off;");

            foreach (var tableName in tableNames)
            {
               dbContext.Database.ExecuteSqlRaw($"DELETE FROM `{tableName}`;");
            }
         }
      }
   }
}

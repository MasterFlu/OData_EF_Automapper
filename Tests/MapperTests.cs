using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.DTOs.Interfaces;
using Example.Entities.Database;
using Example.Entities.Models;
using Example.Tests.Application;
using Example.Tests.Initializers;
using Microsoft.Extensions.DependencyInjection;
using Assert = Xunit.Assert;

namespace Example.Tests
{
   public class MapperTests : IDisposable
   {
      private readonly TestApplication<Program> _factory;
      private readonly DataInitializer _dataInitializer;

      public MapperTests()
      {
         _factory = new TestApplication<Program>();
         _dataInitializer = new DataInitializer(_factory);
      }

      public void Dispose()
      {
         _dataInitializer.Dispose();
      }

      [Fact]
      public void Project_TProcess_To_IProcessDTO()
      {
         using (var scope = _factory.Services.CreateScope())
         {
            _dataInitializer.CreateData();

            var dbContext = scope.ServiceProvider.GetService<ExampleDbContext>()!;
            var mapper = _factory.Services.GetService<IMapper>()!;
            var result = dbContext.Processes.ProjectTo<IProcessDTO>(mapper.ConfigurationProvider);
         }
      }


      [Fact]
      public void Map_Process_To_IProcessDTO()
      {
         Process process = new Process
         {
            Name = "test",
            Number = "1",
         };

         var mapper = _factory.Services.GetService<IMapper>();
         IProcessDTO dto = mapper!.Map<IProcessDTO>(process);

         //Assert.NotNull(dto.Contact);
         Assert.Equal("test", dto.Name);
         Assert.Equal("1", dto.Number);
         Assert.Null(dto.Comments);
      }

      [Fact]
      public void Map_TTimeSheet_To_ITimeSheetDTO()
      {
         DateTime start = DateTime.Now;

         TimeSheet timeSheet = new TimeSheet
         {
            Name = "test",
            Number = "1",
            Process = new Process
            {
               Name = "test",
               Number = "1",
            },
            StartDateTime = start,
            EndDateTime = start.AddMinutes(30)
         };

         var mapper = _factory.Services.GetService<IMapper>();
         ITimeSheetDTO dto = mapper!.Map<ITimeSheetDTO>(timeSheet);

         Assert.Equal("test", dto.Name);
         Assert.Null(dto.Comments);
         Assert.NotNull(dto.Process);
         Assert.Equal(start, dto.StartDateTime);
         Assert.Equal(start.AddMinutes(30), dto.EndDateTime);
      }
   }
}

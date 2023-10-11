using Example.DTOs.Models;
using Example.Tests.Application;
using Example.Tests.Initializers;
using Hsetu.TimeSheet.DTOs.OData;
using System.Net.Http.Json;
using Assert = Xunit.Assert;

namespace Example.Tests
{
   public class ODataProcessTests : IDisposable
   {
      private readonly TestApplication<Program> _factory;
      private readonly DataInitializer _dataInitializer;

      public ODataProcessTests()
      {
         _factory = new TestApplication<Program>();
         _dataInitializer = new DataInitializer(_factory);
      }

      public void Dispose()
      {
         _dataInitializer.Dispose();
      }

      [Fact]
      public async Task Get_Meta_Data()
      {
         var client = _factory.CreateClient();
         var result = await client.GetAsync("/odata/$metadata");
         Assert.True(result.IsSuccessStatusCode);
         string xml = await result.Content.ReadAsStringAsync();
         Assert.NotNull(xml);
      }

      [Fact]
      public async Task Get_Processes_Count_AS_Int()
      {
         // Create 10 processes in dbs
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/ProcessOData/$count");
         Assert.True(result.IsSuccessStatusCode);

         var content = await result.Content.ReadAsStringAsync();
         Assert.NotNull(content);
         Assert.Equal(10, int.Parse(content));
      }

      [Fact]
      public async Task Get_Processes_Count_FROM_ODataResponse()
      {
         // Create 10 processes in dbs
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/ProcessOData/?$count=true&$top=0");
         Assert.True(result.IsSuccessStatusCode);

         var content = await result.Content.ReadFromJsonAsync<ODataResponse<TimeSheetDTO>>();
         Assert.NotNull(content);
         Assert.NotNull(content.Count);
         Assert.Equal(10, content.Count);
      }

      [Fact]
      public async Task Get_Process_One()
      {
         // Create 10 processes in db
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/ProcessOData/?$filter=Name eq 'Process 1'");
         Assert.True(result.IsSuccessStatusCode);

         var content = await result.Content.ReadFromJsonAsync<ODataResponse<TimeSheetDTO>>();
         
         Assert.NotNull(content);
         Assert.NotNull(content.Value);
         Assert.Single(content.Value);
      }

      [Fact]
      public async Task Get_Process_One_With_Timesheets()
      {
         // Create 10 processes in db
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/ProcessOData/?$expand=TimeSheets&$filter=Name eq 'Process 1'");
         Assert.True(result.IsSuccessStatusCode, await result.Content.ReadAsStringAsync());

         var content = await result.Content.ReadFromJsonAsync<ODataResponse<TimeSheetDTO>>();

         Assert.NotNull(content);
         Assert.NotNull(content.Value);
         Assert.Single(content.Value);
      }
   }
}
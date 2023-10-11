using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Assert = Xunit.Assert;
using Timesheet = Example.Entities.Models.TimeSheet;
using Example.Tests.Application;
using Example.Tests.Initializers;
using Hsetu.TimeSheet.DTOs.OData;
using Example.DTOs.Models;

namespace Example.Tests
{
   public class ODataTimesheetTests : IDisposable
   {
      private readonly TestApplication<Program> _factory;
      private readonly DataInitializer _dataInitializer;

      public ODataTimesheetTests()
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
      public async Task Get_Timesheet_Count_AS_Int()
      {
         // Create 10 processes in db
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/TimeSheetOData/$count");
         Assert.True(result.IsSuccessStatusCode, await result.Content.ReadAsStringAsync());

         var content = await result.Content.ReadAsStringAsync();
         Assert.NotNull(content);
         Assert.Equal(20, int.Parse(content));
      }

      [Fact]
      public async Task Get_Timesheet_Count_FROM_ODataResponse()
      {
         // Create 10 processes in db
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/TimeSheetOData/?$count=true&$top=0");
         Assert.True(result.IsSuccessStatusCode, await result.Content.ReadAsStringAsync());

         var content = await result.Content.ReadFromJsonAsync<ODataResponse<Timesheet>>();
         Assert.NotNull(content);
         Assert.NotNull(content.Count);
         Assert.Equal(20, content.Count);
      }

      [Fact]
      public async Task Get_All_Timesheets()
      {
         // Create 10 processes in db
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/TimeSheetOData");
         Assert.True(result.IsSuccessStatusCode);

         var content = await result.Content.ReadFromJsonAsync<ODataResponse<TimeSheetDTO>>();
         Assert.NotNull(content);
         Assert.NotNull(content.Value);
         Assert.Equal(20, content.Value.Count());
      }

      [Fact]
      public async Task Get_Timesheets_To_Process_One()
      {
         // Create 10 processes in db
         _dataInitializer.CreateData();

         var client = _factory.CreateClient();
         HttpResponseMessage result = await client.GetAsync("/odata/TimeSheetOData/?$expand=Process");
         Assert.True(result.IsSuccessStatusCode);

         var options = new JsonSerializerOptions();
         options.Converters.Add(new JsonStringEnumConverter());
         options.PropertyNameCaseInsensitive = true;

         var content = await result.Content.ReadFromJsonAsync<ODataResponse<TimeSheetDTO>>();
         Assert.NotNull(content);

         //Assert.Single(content.Value);
         //Assert.NotNull(content.Value[0].Process);
      }
   }
}

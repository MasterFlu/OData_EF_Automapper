using Example.Entities.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example.Tests.Application
{
   public class TestApplication<TProgram> : WebApplicationFactory<TProgram>
       where TProgram : class
   {
      protected override IHost CreateHost(IHostBuilder builder)
      {
         builder.ConfigureServices(services =>
         {
            services.AddHttpClient();
            services.AddDbContext<ExampleDbContext>();
         });

         return base.CreateHost(builder);
      }
   }
}

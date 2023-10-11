using Example.CQRS;
using Example.DTOs.Interfaces;
using Example.DTOs.Models;
using Example.Entities.Database;
using Example.Entities.OData;
using Example.Entities.OData.Serializers;
using Example.Mappers;
using Example.Server;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors();

#region MediatR

// Add MediatR: Register pipelines in the order they should be executed or handled
builder.Services.AddMediatR(cfg =>
{
   // Bind assembly to mediatr
   cfg.RegisterServicesFromAssemblies(typeof(CQRSExampleEntry).Assembly);
});

#endregion

#region Database

// Add DB
builder.Services.AddDbContext<ExampleDbContext>();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(MapperEntryPoint));

#endregion

var odataBatchHandler = new DefaultODataBatchHandler();

// Add Api-Controllers and  OData-Controllers
builder.Services.AddControllers()
   .AddOData(options => options
      .EnableQueryFeatures(100)
      .AddRouteComponents("odata", EdmModelOdata.GetEdmModel(),
         builder => {
            // Here I am trying to add a custom Serializer to solve Process->$expand->Timesheets
            builder.AddSingleton<DefaultODataSerializerProvider>(serviceprovider => new ITimeSheetDTOResourceSerializerProvider(serviceprovider));
         }
      )
    )
   .AddNewtonsoftJson(options =>
   {
      // Configure a custom converter
      options.SerializerSettings.Converters.Add(new WAppConverter<IProcessDTO, ProcessDTO>());
      options.SerializerSettings.Converters.Add(new WAppConverter<ITimeSheetDTO, TimeSheetDTO>());
   });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();

   app.UseCors(x => x.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
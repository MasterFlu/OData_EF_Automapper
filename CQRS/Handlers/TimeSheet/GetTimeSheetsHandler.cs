using AutoMapper;
using AutoMapper.AspNet.OData;
using Example.CQRS.Queries.TimeSheet;
using Example.DTOs.Models;
using Example.Entities.Database;
using MediatR;

namespace Example.CQRS.Handlers.TimeSheet
{
   public class GetTimeSheetsHandler : IRequestHandler<GetTimeSheetsQuery, IQueryable<TimeSheetDTO>>
   {
      private readonly ExampleDbContext _dbContext;
      private readonly IMapper _mapper;

      public GetTimeSheetsHandler(ExampleDbContext dbContext, IMapper mapper) 
      { 
         _dbContext = dbContext;
         _mapper = mapper;
      }

      public Task<IQueryable<TimeSheetDTO>> Handle(GetTimeSheetsQuery request, CancellationToken cancellationToken)
      {
         IQueryable<TimeSheetDTO> query = _dbContext.TimeSheets.GetQuery(_mapper, request.Options);
         return Task.FromResult(query);
      }
   }
}

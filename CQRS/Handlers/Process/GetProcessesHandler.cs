using AutoMapper;
using AutoMapper.AspNet.OData;
using Example.CQRS.Queries.Process;
using Example.DTOs.Models;
using Example.Entities.Database;
using MediatR;

namespace Example.CQRS.Handlers.Process
{
   public class GetProcessesHandler : IRequestHandler<GetProcessesQuery, IQueryable<ProcessDTO>>
   {
      private readonly ExampleDbContext _dbContext;
      private readonly IMapper _mapper;

      public GetProcessesHandler(ExampleDbContext dbContext, IMapper mapper)
      {
         _dbContext = dbContext;
         _mapper = mapper;
      }

      public async Task<IQueryable<ProcessDTO>> Handle(GetProcessesQuery request, CancellationToken cancellationToken)
      {
         IQueryable<ProcessDTO> query = await _dbContext.Processes.GetQueryAsync(_mapper, request.Options);
         return query;
      }
   }
}

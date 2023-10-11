using Example.CQRS.Queries.Process;
using Example.DTOs.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Example.Server.Controllers.Process
{
   public class ProcessODataController : ODataController
   {
      private readonly IMediator _mediator;

      public ProcessODataController(IMediator mediator)
      {
         _mediator = mediator;
      }

      [EnableQuery]
      public async Task<IQueryable<ProcessDTO>> Get(ODataQueryOptions<ProcessDTO> queryOptions)
      {
         var result = await _mediator.Send(new GetProcessesQuery(queryOptions)).ConfigureAwait(false); 
         return result;
      }

      [EnableQuery]
      public async Task<IActionResult> Get([FromRoute] Guid key, ODataQueryOptions<ProcessDTO> queryOptions)
      { 
         return Ok((await _mediator.Send(new GetProcessesQuery(queryOptions)).ConfigureAwait(false)).Where(u => u.Id == key));
      }
   }
}

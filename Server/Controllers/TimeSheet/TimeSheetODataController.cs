using Example.CQRS.Queries.TimeSheet;
using Example.DTOs.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.UriParser;

namespace Example.Server.Controllers.TimeSheet
{
   public class TimeSheetODataController : ODataController
   {
      private readonly IMediator _mediator;

      public TimeSheetODataController(IMediator mediator)
      {
         _mediator = mediator;
      }

      [EnableQuery]
      public async Task<IQueryable<TimeSheetDTO>> Get(ODataQueryOptions<TimeSheetDTO> options)
      {
         var result = await _mediator.Send(new GetTimeSheetsQuery(options)).ConfigureAwait(false);
         return result;
      }

      [EnableQuery]
      public async Task<IQueryable<TimeSheetDTO>> Get([FromRoute] Guid key, ODataQueryOptions<TimeSheetDTO> options)
      {
         return (await _mediator.Send(new GetTimeSheetsQuery(options)).ConfigureAwait(false)).Where(u => u.Id == key);
      }
   }
}

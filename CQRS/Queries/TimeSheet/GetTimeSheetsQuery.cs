using Example.DTOs.Models;
using MediatR;
using Microsoft.AspNetCore.OData.Query;

namespace Example.CQRS.Queries.TimeSheet
{
   public record GetTimeSheetsQuery(ODataQueryOptions<TimeSheetDTO> Options) : IRequest<IQueryable<TimeSheetDTO>>;
}

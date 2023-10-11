using Example.DTOs.Models;
using MediatR;
using Microsoft.AspNetCore.OData.Query;

namespace Example.CQRS.Queries.Process
{
   public record GetProcessesQuery(ODataQueryOptions<ProcessDTO> Options) : IRequest<IQueryable<ProcessDTO>>;
}

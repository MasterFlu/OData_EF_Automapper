using AutoMapper;
using Example.DTOs.Interfaces;
using Example.DTOs.Models;

namespace Example.Mappers.Process
{
   public class ProcessProfile : Profile
   {
      public ProcessProfile() 
      {
            CreateMap<Entities.Models.Process, ProcessDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UId))
            .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<Entities.Models.Process, IProcessDTO>()
            .As<ProcessDTO>();

         CreateMap<IProcessDTO, ProcessDTO>();
      }
   }
}

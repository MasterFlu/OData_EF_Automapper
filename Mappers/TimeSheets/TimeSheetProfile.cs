using AutoMapper;
using Example.DTOs.Interfaces;
using Example.DTOs.Models;
using Timesheet = Example.Entities.Models.TimeSheet;

namespace Example.Mappers.TimeSheets
{
   public class TimeSheetProfile : Profile
   {
      public TimeSheetProfile() 
      {
         CreateMap<Timesheet, TimeSheetDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UId))
            .ForAllMembers(o => o.ExplicitExpansion());

         CreateMap<Timesheet, ITimeSheetDTO>()
            .As<TimeSheetDTO>();

         //CreateMap<ITimeSheetDTO, ITimeSheetDTO>()
         //   .As<TimeSheetDTO>();

         CreateMap<ITimeSheetDTO, TimeSheetDTO>();
      }
   }
}

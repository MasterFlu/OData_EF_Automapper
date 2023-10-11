using Example.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Example.DTOs.Models
{
   public class TimeSheetDTO : ITimeSheetDTO
   {
      [Key]
      public Guid Id { get; set; }
      public string? Comments { get; set; }
      public string? Name { get; set; }
      public int Type { get; set; }
      public Guid? ProcessUId { get; set; } = null;
      public IProcessDTO? Process { get; set; } = null;
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
   }
}

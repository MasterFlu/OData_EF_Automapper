using Example.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Example.DTOs.Models
{
   public class ProcessDTO : IProcessDTO
   {
      [Key]
      public Guid Id { get; set; }
      public string? Number { get; set; }
      public string? Name { get; set; }
      public string? Comments { get; set; }
      public virtual ICollection<ITimeSheetDTO>? TimeSheets { get; set; }
   }
}
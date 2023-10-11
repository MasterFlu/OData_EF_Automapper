
using System.ComponentModel.DataAnnotations;

namespace Example.DTOs.Interfaces
{
   public interface ICoreDTO
   {
      [Key]
      Guid Id { get; set; }

      string? Comments { get; set; }
   }
}

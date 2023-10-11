using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Example.Entities.Models.Interfaces;

namespace Example.Entities.Models.Core
{
   public abstract class ModelEntityCore : IModelEntityCore
   {
      [Key]
      public Guid UId { get; set; }
      public Guid? GId { get; set; }
      public DateTime? StampEdit { get; set; }
      public DateTime? StampCreate { get; set; }
      public DateTime? StampSync { get; set; }

      public ModelEntityCore()
      {
         UId = Guid.NewGuid();
      }

      [NotMapped]
      public string Id
      {
         get => UId.ToString("D").ToUpperInvariant();
         set { UId = Guid.Parse(value); }
      }

      [NotMapped]
      public byte[] Version
      {
         get => Encoding.UTF8.GetBytes(EntityTag ?? string.Empty);

         set { EntityTag = Encoding.UTF8.GetString(value); }
      }

      [NotMapped]
      public string EntityTag { get; set; } = string.Empty;

      [NotMapped]
      public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;


      [NotMapped]
      public bool Deleted { get; set; } = false;


      public bool Equals(IModelEntityCore? other)
        => other != null
        && Id == other.Id
        && UpdatedAt == other.UpdatedAt
        && Deleted == other.Deleted
        && Version.SequenceEqual(other.Version);
   }
}

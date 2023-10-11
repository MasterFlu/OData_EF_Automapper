namespace Example.Entities.Models.Interfaces
{
   public interface IModelEntityCore : IEquatable<IModelEntityCore>
   {
      string Id { get; set; }

      byte[] Version { get; set; }

      DateTimeOffset UpdatedAt { get; set; }

      bool Deleted { get; set; }
   }
}

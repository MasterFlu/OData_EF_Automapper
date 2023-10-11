namespace Example.DTOs.Interfaces
{
   public interface IProcessDTO : ICoreDTO
   {
      string? Number { get; set; }

      string? Name { get; set; }
      ICollection<ITimeSheetDTO>? TimeSheets { get; set; }
   }
}

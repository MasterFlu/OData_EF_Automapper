namespace Example.DTOs.Interfaces
{
   public interface ITimeSheetDTO : ICoreDTO
   {
      string? Name { get; set; }
      int Type { get; set; }
      Guid? ProcessUId { get; set; }
      IProcessDTO? Process { get; set; }
      DateTime StartDateTime { get; set; }
      DateTime EndDateTime { get; set; }
   }
}

using Example.Entities.Models.Core;

namespace Example.Entities.Models
{
   public class Process : ModelEntity
   {
      /// <summary>
      /// Process can have 0 to n Timesheets
      /// </summary>
      public virtual ICollection<TimeSheet>? TimeSheets { get; set; } = null;

      /// <summary>
      /// Ctr
      /// </summary>
      public Process() : base() { }
   }
}

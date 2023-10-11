using Example.Entities.Models.Core;

namespace Example.Entities.Models
{
   public class TimeSheet : ModelEntity
   {
      /// <summary>
      /// Timesheet can be assigned to 0 to 1 Process
      /// </summary>
      public Process? Process { get; set; } = null;

      public DateTime StartDateTime { get; set; }

      public DateTime EndDateTime { get; set; }

      /// <summary>
      /// Ctr
      /// </summary>
      public TimeSheet() : base() 
      {
      }
   }
}
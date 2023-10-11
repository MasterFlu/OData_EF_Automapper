using Example.DTOs.Interfaces;
using Example.DTOs.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Example.Entities.OData
{
   public class EdmModelOdata
   {
      public static IEdmModel GetEdmModel()
      {
         ODataConventionModelBuilder builder = new();
         builder.EnableLowerCamelCase();

         builder.ComplexType<ICoreDTO>();
         builder.EntityType<IProcessDTO>();
         builder.EntityType<ITimeSheetDTO>();

         #region timesheet_definition

         // If We call our timesheet OData route we want to return the DTO and not the Entity
         EntitySetConfiguration<TimeSheetDTO> timesheetDTOs = builder.EntitySet<TimeSheetDTO>("TimeSheetOData");

         //Define NavigationProperties for expand
         timesheetDTOs.EntityType.HasOptional(t => t.Process, (t, e) => t.ProcessUId == e.Id);

         #endregion

         #region process_definition

         EntitySetConfiguration<ProcessDTO> processDTOs = builder.EntitySet<ProcessDTO>("ProcessOData");

         //Define NavigationProperties for expand
         processDTOs.EntityType.HasOptional(p => p.TimeSheets).Select(SelectExpandType.Allowed);

         #endregion

         return builder.GetEdmModel();
      }
   }
}

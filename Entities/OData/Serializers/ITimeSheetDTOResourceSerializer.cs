using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData;
using Microsoft.OData;


namespace Example.Entities.OData.Serializers
{
   public class ITimeSheetDTOResourceSerializer : ODataResourceSerializer
   {
      public ITimeSheetDTOResourceSerializer(ODataSerializerProvider serializerProvider)
          : base(serializerProvider)
      { }

      public override ODataResource CreateResource(SelectExpandNode selectExpandNode, ResourceContext resourceContext)
      {
         ODataResource resource = base.CreateResource(selectExpandNode, resourceContext);
         resource.Properties.ToList().ForEach(p => p.Name = p.Name.ToUpper());
         return resource;
      }
   }
}

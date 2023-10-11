using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData.Edm;

namespace Example.Entities.OData.Serializers
{
   public class ITimeSheetDTOResourceSerializerProvider : DefaultODataSerializerProvider
   {
      public ITimeSheetDTOResourceSerializerProvider(IServiceProvider serviceProvider)
         : base(serviceProvider)
      {
         string s = "";
      }

      public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
      {
         if (edmType.IsEntity() || edmType.IsComplex())
         {
            return new ITimeSheetDTOResourceSerializer(this);
         }
         return base.GetEdmTypeSerializer(edmType);
      }
   }
}

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Example.Server
{
   public class WAppConverter<I, T> : JsonConverter
   {
      public override bool CanConvert(Type objectType)
      {
         return (objectType == typeof(I));
      }

      public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
      {
         try
         {
            JObject jo = JObject.Load(reader);
            return jo.ToObject<T>(serializer);
         }
         catch
         {
            return Activator.CreateInstance<T>();
         }
      }

      public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
      {
         throw new NotImplementedException();
      }
   }
}

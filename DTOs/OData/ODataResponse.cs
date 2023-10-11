using System.Text.Json.Serialization;

namespace Hsetu.TimeSheet.DTOs.OData
{
   public class ODataResponse<T>
   {
      [JsonPropertyName("@odata.context")]
      public string? Context { get; set; }
      [JsonPropertyName("@odata.count")]
      public int? Count { get; set; }
      [JsonPropertyName("value")]
      public T[]? Value { get; set; }
   }
}

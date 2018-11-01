using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Academy.Core.Extensions
{
    public static class StringExtentions
    {
        public static string ToJson(this object self, bool useCamelCaseResolver = false)
        {
            if (self == null)
                return null;

            if (!useCamelCaseResolver) return JsonConvert.SerializeObject(self);

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(self, jsonSettings);
        }
    }
}

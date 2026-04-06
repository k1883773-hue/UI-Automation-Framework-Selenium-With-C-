using Newtonsoft.Json;

namespace NaveenAutomationPOM.Utilities
{
    public static class JsonHelper
    {
        public static T ReadJson<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }
}

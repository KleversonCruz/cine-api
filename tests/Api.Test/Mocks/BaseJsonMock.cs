using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Api.Test.Mocks
{
    public abstract class BaseJsonMock
    {
        protected string BasePath = Directory.GetCurrentDirectory();
        protected string JsonString { get; set; }

        protected BaseJsonMock(string jsonPath)
        {
            JsonString = File.ReadAllTextAsync(BasePath + jsonPath).Result;
        }

        public virtual List<T> GetListAsync<T>()
        {
            return Deserialize<List<T>>(JsonString);
        }

        public virtual T Get<T>(int index)
        {
            return Deserialize<List<T>>(JsonString)[index];
        }

        protected static T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text)!;
        }
    }
}

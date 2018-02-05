using System;
using System.Text;
using LitJson;

namespace Juniansoft.Termission.Core.Services
{
    public class LitJsonService: IJsonService
    {
        public T Deserialize<T>(string json)
        {
            return LitJson.JsonMapper.ToObject<T>(json);
        }

        public string Serialize<T>(T obj, bool pretty=false)
        {
            var sb = new StringBuilder();
            var writer = new JsonWriter(sb)
            {
                PrettyPrint = pretty,
            };
            if (pretty)
                writer.IndentValue = 2;
            LitJson.JsonMapper.ToJson(obj, writer);
            return sb.ToString();
        }
    }
}

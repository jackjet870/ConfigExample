using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace JT.THB.ThirdPart.Config.Utilities
{
    public class JsonHelper
    {
        public static string ToJsJson(object item)
        {
            var serializer = new DataContractJsonSerializer(item.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                var sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        public static T JsonDeserialize<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof (T));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var obj = (T) ser.ReadObject(ms);

            return obj;
        }
    }
}
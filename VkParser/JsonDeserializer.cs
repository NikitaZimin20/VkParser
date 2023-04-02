using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkParser.src
{
    internal class JsonDeserializer
    {
        private string ReadText()
        {
            string path = ConfigurationManager.ConnectionStrings["JsonConnection"].ConnectionString;
            string text = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
        public T DeserializeByName<T>(string name)  
        {
            var json = ReadText();
            JObject obj = JObject.Parse(json);
            dynamic result =obj[name] ;
            return result.ToObject<T>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson.Models
{
    public class JObject : JParser
    {
        private List<KeyValuePair<string, string>> _fields = new List<KeyValuePair<string, string>>();

        public JObject(string json)
        {
            json = GetInsideByTags(json, '{', '}');
            foreach (var field in json.Split(','))
            {
                var pair = field.Replace("\"", string.Empty).Split(':');
                _fields.Add(new KeyValuePair<string, string>(pair[0], pair[1]));
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetFields() => _fields;
    }
}

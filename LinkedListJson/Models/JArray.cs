using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson.Models
{
    public class JArray : JParser
    {
        private List<JObject> _jObjects = new List<JObject>();

        public JArray(string json)
        {
            json = GetInsideByTags(json, '[', ']');
            foreach (var objJson in SplitObjectsByTags(json, '{', '}', ','))
                _jObjects.Add(new JObject(objJson));
        }

        public IEnumerable<JObject> GetJObjects() => _jObjects;
    }
}

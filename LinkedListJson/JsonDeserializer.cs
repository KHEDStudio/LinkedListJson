using LinkedListJson.Interfaces;
using LinkedListJson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson
{
    public class JsonDeserializer<TValue> : IDeserializer<TValue> where TValue : new()
    {
        private Dictionary<Guid, TValue> _nodes = new Dictionary<Guid, TValue>();

        public IEnumerable<TValue> Deserialize(string data)
        {
            _nodes.Clear();
            var array = new JArray(data.Trim());
            foreach (var obj in array.GetJObjects())
                yield return GetSerializedInstance(obj);
        }

        private TValue GetSerializedInstance(JObject obj)
        {
            TValue instance = default;
            foreach (var field in obj.GetFields())
            {
                if (field.Key.Equals(Settings.GuidHeader))
                    instance = GetNodeInstance(Guid.Parse(field.Value));
                else if (field.Key.Contains(Settings.GuidHeader))
                {
                    var key = field.Key.Replace(Settings.GuidHeader, string.Empty);
                    typeof(TValue).GetField(key).SetValue(instance, GetNodeInstance(Guid.Parse(field.Value)));
                }
                else typeof(TValue).GetField(field.Key).SetValue(instance, field.Value);
            }
            return instance;
        }

        private TValue GetNodeInstance(Guid guid)
        {
            if (_nodes.ContainsKey(guid))
                return _nodes[guid];
            var node = new TValue();
            _nodes.Add(guid, node);
            return node;
        }
    }
}

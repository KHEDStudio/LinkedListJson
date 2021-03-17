using LinkedListJson.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListJson
{
    public class JsonSerializer<TValue> : ISerializer<TValue>
    {
        private StringBuilder _linkedListJson = null;
        private Dictionary<TValue, Guid> _nodes = new Dictionary<TValue, Guid>();

        public void AddForSerialize(TValue value)
        {
            var nodeGuid = GetNodeGuid(value);
            var valueJson = GetSerializedFields(value);
            _linkedListJson = (_linkedListJson == null) ? new StringBuilder("[") : _linkedListJson.Append(',');
            _linkedListJson.Append('{').Append($"\"{Settings.GuidHeader}\":\"{nodeGuid}\",")
                .Append(valueJson).Append('}');
        }

        private string GetSerializedFields(object model)
        {
            StringBuilder builder = null;
            foreach (var field in model.GetType().GetFields())
            {
                builder = (builder == null) ? new StringBuilder() : builder.Append(',');
                if (field.FieldType == typeof(TValue))
                    builder.Append($"\"{Settings.GuidHeader}{field.Name}\":\"{GetNodeGuid((TValue)field.GetValue(model))}\"");
                else
                    builder.Append($"\"{field.Name}\":\"{field.GetValue(model)}\"");
            }
            return builder.ToString();
        }

        private Guid GetNodeGuid(TValue value)
        {
            if (_nodes.ContainsKey(value))
                return _nodes[value];
            var nodeGuid = Guid.NewGuid();
            _nodes.Add(value, nodeGuid);
            return nodeGuid;
        }

        public string GetSerializedData() => _linkedListJson?.Append(']')?.ToString() ?? "{}";

        public void Clear()
        {
            _linkedListJson = null;
            _nodes.Clear();
        }
    }
}

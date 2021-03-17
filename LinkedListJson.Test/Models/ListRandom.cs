using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson.Test.Models
{
    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            var serializer = new JsonSerializer<ListNode>();
            for (var node = Head; node != Tail; node = node.Next)
                serializer.AddForSerialize(node);
            serializer.AddForSerialize(Tail);
            using (var writer = new StreamWriter(s, Encoding.UTF8, leaveOpen: true))
                writer.Write(serializer.GetSerializedData());
        }

        public void Deserialize(Stream s)
        {
            try
            {
                var deserializer = new JsonDeserializer<ListNode>();
                using (var reader = new StreamReader(s, Encoding.UTF8, leaveOpen: true))
                {
                    var nodes = deserializer.Deserialize(reader.ReadToEnd()).ToArray();
                    Head = nodes.First();
                    Tail = nodes.Last();
                    Count = nodes.Length;
                }
            }
            catch
            {
                throw new ArgumentException("The input string was not in the correct format");
            }
        }
    }
}

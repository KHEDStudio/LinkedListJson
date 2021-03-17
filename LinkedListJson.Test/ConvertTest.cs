using LinkedListJson.Models;
using LinkedListJson.Test.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LinkedListJson.Test
{
    public class ConvertTest
    {
        private ListRandom _listRandom = new ListRandom();

        [SetUp]
        public void Setup()
        {
            var nodes = new List<ListNode>();
            for (int i = 0; i < 1e6; i++)
            {
                var node = new ListNode();
                node.Data = $"test_data_{i}";
                nodes.Add(node);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Next = nodes[(i + 1) % nodes.Count];
                nodes[i].Previous = nodes[(i - 1 < 0) ? nodes.Count - 1 : i - 1];
                nodes[i].Random = nodes[(0, nodes.Count).RandomNumber()];
            }

            _listRandom.Count = nodes.Count;
            _listRandom.Head = nodes.First();
            _listRandom.Tail = nodes.Last();
        }

        [Test]
        public void Test1()
        {
            var listRandom = new ListRandom();
            using (var stream = new MemoryStream())
            {
                _listRandom.Serialize(stream);
                stream.Position = 0;

                listRandom.Deserialize(stream);
            }

            var _node = _listRandom.Head;
            var node = listRandom.Head;

            for (int i = 0; i < _listRandom.Count; i++, _node = _node.Next, node = node.Next)
            {
                Assert.AreEqual(_node.Data, node.Data);
                Assert.AreEqual(_node.Next.Data, node.Next.Data);
                Assert.AreEqual(_node.Previous.Data, node.Previous.Data);
                Assert.AreEqual(_node.Random.Data, node.Random.Data);
            }
        }
    }
}
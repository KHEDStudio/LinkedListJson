using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson.Interfaces
{
    public interface IDeserializer<TValue>
    {
        public IEnumerable<TValue> Deserialize(string data);
    }
}

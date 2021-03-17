using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson.Interfaces
{
    public interface ISerializer<TValue>
    {
        public void AddForSerialize(TValue value);
        public string GetSerializedData();
        public void Clear();
    }
}

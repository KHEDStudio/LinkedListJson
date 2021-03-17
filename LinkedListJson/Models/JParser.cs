using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListJson.Models
{
    public abstract class JParser
    {
        protected string GetInsideByTags(string json, char openObjTag, char closeObjTag)
        {
            int offset = 1;
            int counter = 0, startIndex = json.IndexOf(openObjTag) + offset;
            for (int i = 0; i < json.Length; i++)
            {
                if (json[i].Equals(openObjTag))
                    counter++;
                if (json[i].Equals(closeObjTag))
                    counter--;
                if (counter == 0)
                    return json.Substring(startIndex, json.Length - startIndex - offset);
            }
            return string.Empty;
        }

        protected IEnumerable<string> SplitObjectsByTags(string json, char openObjTag, char closeObjTag, char objSeparator)
        {
            int offset = 1;
            int counter = 0, startIndex = 0;
            for (int i = 0; i < json.Length; i++)
            {
                if (json[i].Equals(openObjTag))
                    counter++;
                if (json[i].Equals(closeObjTag))
                    counter--;
                if (counter == 0 && json[i].Equals(objSeparator))
                {
                    yield return json.Substring(startIndex, i - startIndex);
                    startIndex = i + offset;
                }
            }

            yield return json.Substring(startIndex);
        }
    }
}

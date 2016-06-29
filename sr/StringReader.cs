using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sr
{
    public class StringReader
    {
        public int Index;
        public string Data;

        public StringReader(string data)
        {
            int length = data.Length;
            bool prevWhiteSpace = false;
            List<char> ldata = new List<char>(length);

            for (int i = 0; i < length; i++)
            {
                char c = data[i];
                bool IsWhiteSpace = char.IsWhiteSpace(c);
                if (!(prevWhiteSpace && IsWhiteSpace))
                {
                    ldata.Add(c);
                }
                prevWhiteSpace = IsWhiteSpace;
            }

            if (ldata.Count != length)
                data = new string(ldata.ToArray());
            ldata = null;

            Data = data;
        }

        public string[] ReadNextWords(uint Quantity = 2)
        {
            string[] words = new string[Quantity];

            for (uint i = 0; i < Quantity; i++)
            {
                words[i] = ReadNextWord();
            }

            return words;
        }

        public bool StringCompare(ref string[] a, ref string[] b)
        {
            if (a == b)
                return true;

            if (a == null || b == null)
                return false;            

            if (a.Length != b.Length)
                return false;

            int length = a.Length;

            for (int i = 0; i < length; i++)
            {
                if(string.CompareOrdinal(a[i], b[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Match(string[] data, params string[][] documents)
        {
            for (int i = 0; i < documents.Length; i++)
            {
                if(StringCompare(ref documents[i], ref data))
                {
                    return true;
                }
            }
            return false;
        }

        public string ReadNextWord()
        {
            int length = Data.Length;            
            int NewIndex = length - 1;
            string data = string.Empty;

            for (int i = Index; i < length; i++)
            {
                if(char.IsWhiteSpace(Data[i]))
                {
                    NewIndex = i;
                    break;
                }
            }
            length = NewIndex - Index;
            if(length > 0)
            {            
                data = Data.Substring(Index, length);
            }
            Index = NewIndex + 1;
            return data;
        }
    }
}

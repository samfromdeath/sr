using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sr
{
    public class Request
    {
        public Request(string Data)
        {
            try
            {
                var stringR = new StringReader(Data.ToLower());

                if (stringR.ReadNextWord() == "list")
                {
                    string source = stringR.ReadNextWord();
                    if (source.Length == 0)
                        throw new Exception("no data");

                    if (stringR.Match(stringR.ReadNextWords(2)
                            , new[] { "that", "match" }, new[] { "that", "matches" }))
                    {
                        Console.WriteLine("{0} data");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }            
        }
    }
}

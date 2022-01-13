using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpLexer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> strings = new List<string>();

            using (StreamReader inputStream = new StreamReader("../../../input.txt"))
            {
                String line;
                while ((line = inputStream.ReadLine()) != null)
                {
                    strings.Add(line);
                }
            }

            List<TokenDetalization> tokenDetalizations = new TokenSсanner().Scan(strings);

            using (StreamWriter outputStream = new StreamWriter("../../../output.txt"))
            {
                foreach (TokenDetalization tokenDetalization in tokenDetalizations)
                {
                    tokenDetalization.PrintTokenDetalization(outputStream);
                }
            }
        }
    }
}
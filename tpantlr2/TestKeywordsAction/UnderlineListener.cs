using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKeywordsAction
{
    public class UnderlineListener : BaseErrorListener
    {
        public override void SyntaxError(IRecognizer recognizer,
                    IToken offendingSymbol,
                    int line, int charPositionInLine,
                    string msg,
                    RecognitionException e)
        {
            Console.WriteLine($"line {line}:{charPositionInLine} {msg}");
            UnderlineError(recognizer, offendingSymbol, line, charPositionInLine);
        }

        protected void UnderlineError(IRecognizer recognizer,
                                      IToken offendingToken, 
                                      int line, int charPositionInLine)
        {
            CommonTokenStream tokens = (CommonTokenStream)recognizer.InputStream;
            string? input = tokens.TokenSource.InputStream.ToString();
            if (input != null)
            {
                string[] lines = input.Split("\n");
                string errorLine = lines[line - 1];
                Console.WriteLine(errorLine);
            }
            
            for (int i = 0; i < charPositionInLine; i++)
            {
                Console.Write(" ");
            }
            int start = offendingToken.StartIndex;
            int stop = offendingToken.StopIndex;
            if (start >= 0 && stop >= 0)
            {
                for (int i = start; i <= stop; i++) Console.Write("^");
            }
            Console.WriteLine();
        }
    
    }
}

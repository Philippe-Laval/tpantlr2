using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPred
{
    public class VerboseListener : BaseErrorListener
    {
        public override void SyntaxError(IRecognizer recognizer,
                                IToken offendingSymbol,
                                int line, int charPositionInLine,
                                string msg,
                                RecognitionException e)
        {
            var r = recognizer as Parser;
            if (r != null)
            {
                IList<string> stack = r.GetRuleInvocationStack();
                var reversedStack = stack.Reverse();
                if (reversedStack != null)
                {
                    var temp = string.Join(", ", reversedStack);
                    Console.WriteLine($"rule stack: {temp}");
                }
            }
            Console.WriteLine($"line {line}:{charPositionInLine} at {offendingSymbol}: {msg}");
        }
    }
}
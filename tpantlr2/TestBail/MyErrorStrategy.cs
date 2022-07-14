using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBail
{
    public class MyErrorStrategy : DefaultErrorStrategy
    {

        protected internal new void ReportNoViableAlternative(Parser parser, NoViableAltException e)
        {
            // ANTLR generates Parser subclasses from grammars and
            // Parser extends Recognizer. Parameter parser is a
            // pointer to the parser that detected the error
            String msg = "can't choose between alternatives"; // nonstandard msg
            parser.NotifyErrorListeners(e.OffendingToken, msg, e);
        }
    
    }
}

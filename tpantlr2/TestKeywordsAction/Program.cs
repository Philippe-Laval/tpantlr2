using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

// OK
// x = 34;

// KO
// if = 34;

// OK
// if 1 then i = 4;

// while 'e' i = 4;

namespace TestKeywordsAction
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            KeywordsLexer lexer = new KeywordsLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            KeywordsParser parser = new KeywordsParser(tokens);

            parser.RemoveErrorListeners(); // remove ConsoleErrorListener
            parser.AddErrorListener(new UnderlineListener()); // add ours

            // Begin parsing at rule stat
            IParseTree tree = parser.stat();


            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // Actions for lexer are in this generated file
            // Look in obj/Debug/net6.0/KeywordsLexer.cs
            // Look in obj/Debug/net6.0/KeywordsParser.cs
        }
    }
}


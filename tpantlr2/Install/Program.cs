using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Install
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("input");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            HelloLexer lexer = new HelloLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            HelloParser parser = new HelloParser(tokens);
            // Begin parsing at rule r
            IParseTree tree = parser.r();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // Look in obj/Debug/net6.0/HelloParser.cs
            // public RContext r()
        }
    }
}


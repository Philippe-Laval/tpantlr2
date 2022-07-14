using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestLibExpr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Correct input");

            Test("t.expr");

            Console.WriteLine("Handling Erroneous Input (parser recovers)");

            Test("t2.expr");
        }

        private static void Test(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            LibExprLexer lexer = new LibExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            LibExprParser parser = new LibExprParser(tokens);
            // Begin parsing at rule prog
            IParseTree tree = parser.prog();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // Look in obj/Debug/net6.0/HelloParser.cs
            // public RContext r()
        }
    }
}
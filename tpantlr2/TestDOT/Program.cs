using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestDOT
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.dot");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            DOTLexer lexer = new DOTLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            DOTParser parser = new DOTParser(tokens);

            // Begin parsing at rule graph
            IParseTree tree = parser.graph();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
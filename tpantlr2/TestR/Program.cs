using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestR
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.R");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            RLexer lexer = new RLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            RParser parser = new RParser(tokens);

            // Begin parsing at rule prog
            IParseTree tree = parser.prog();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.data");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            DataLexer lexer = new DataLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            // pass column number
            DataParser parser = new DataParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
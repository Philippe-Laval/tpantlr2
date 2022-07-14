using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestCymbol
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.cymbol");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            CymbolLexer lexer = new CymbolLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            CymbolParser parser = new CymbolParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            // tree.Inspect(parser); // show in gui
            // tree.Save(parser, "/tmp/R.ps"); // Generate postscript

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestJSONListener
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.json");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            JSONLexer lexer = new JSONLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            JSONParser parser = new JSONParser(tokens);
            parser.BuildParseTree = true;

            // Begin parsing at rule json
            IParseTree tree = parser.json();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            ParseTreeWalker walker = new ParseTreeWalker();
            XMLEmitter converter = new XMLEmitter();
            walker.Walk(converter, tree);
            Console.WriteLine(converter.GetXML(tree));
        }
    }
}
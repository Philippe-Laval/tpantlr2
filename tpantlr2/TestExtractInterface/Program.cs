using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestExtractInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("Demo.java");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            JavaLexer lexer = new JavaLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            JavaParser parser = new JavaParser(tokens);
            // Begin parsing at rule compilationUnit
            IParseTree tree = parser.compilationUnit();

            

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // Look in obj/Debug/net6.0/JavaParser.cs
            // public CompilationUnitContext compilationUnit()

            ParseTreeWalker walker = new ParseTreeWalker(); // create standard walker
            ExtractInterfaceListener extractor = new ExtractInterfaceListener(parser);
            walker.Walk(extractor, tree); // initiate walk of tree with listener
        }
    }
}
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestPropertyFile
{
    class Program
    {
        static void Main(string[] args)
        {
            TestListener();
            TestVisitor();
        }

        static void TestListener()
        {
            using TextReader text_reader = File.OpenText("t.properties");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            PropertyFileLexer lexer = new PropertyFileLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            PropertyFileParser parser = new PropertyFileParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // create a standard ANTLR parse tree walker
            ParseTreeWalker walker = new ParseTreeWalker();
            // create listener then feed to walker
            PropertyFileListener listener = new PropertyFileListener();
            // walk parse tree
            walker.Walk(listener, tree);       
            // print results
            foreach (var t in listener.Props)
            {
                Console.WriteLine($"{t.Key}={t.Value}");
            }
        }

        static void TestVisitor()
        {
            using TextReader text_reader = File.OpenText("t.properties");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            PropertyFileLexer lexer = new PropertyFileLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            PropertyFileParser parser = new PropertyFileParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            PropertyFileVisitor visitor = new PropertyFileVisitor();
            visitor.Visit(tree);

            // print results
            foreach (var t in visitor.Props)
            {
                Console.WriteLine($"{t.Key}={t.Value}");
            }
        }


    }
}
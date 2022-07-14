using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestPropertyFileCoupled
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Using PropertyFileCoupledParser");

            Test1();

            Console.WriteLine("Using PropertyFilePrinterParser");

            Test2();

            Console.WriteLine("Using PropertyFileLoaderParser");

            Test3();
        }

        static void Test1()
        {
            using TextReader text_reader = File.OpenText("t.properties");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            PropertyFileCoupledLexer lexer = new PropertyFileCoupledLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            PropertyFileCoupledParser parser = new PropertyFileCoupledParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }

        static void Test2()
        {
            using TextReader text_reader = File.OpenText("t.properties");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            PropertyFileCoupledLexer lexer = new PropertyFileCoupledLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            PropertyFilePrinterParser parser = new PropertyFilePrinterParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

        }

        static void Test3()
        {
            using TextReader text_reader = File.OpenText("t.properties");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            PropertyFileCoupledLexer lexer = new PropertyFileCoupledLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            PropertyFileLoaderParser parser = new PropertyFileLoaderParser(tokens);

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            foreach(var t in parser.Props)
            {
                Console.WriteLine($"{t.Key}={t.Value}");
            }

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

        }
    }
}
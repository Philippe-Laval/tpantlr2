using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            //TextReader text_reader = File.OpenText("input1");

            //// Create an input character stream from standard in
            //var input = new AntlrInputStream(text_reader);
            //// Create an ExprLexer that feeds from that stream
            //ArrayInitLexer lexer = new ArrayInitLexer(input);
            //// Create a stream of tokens fed by the lexer
            //CommonTokenStream tokens = new CommonTokenStream(lexer);
            //// Create a parser that feeds off the token stream
            //ArrayInitParser parser = new ArrayInitParser(tokens);
            //// Begin parsing at init rule
            //IParseTree tree = parser.init();

            //Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree


            Test("input1");
            Test("input2");
            Test("input3");

            Translate("input1");
        }

        private static void Test(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            ArrayInitLexer lexer = new ArrayInitLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            ArrayInitParser parser = new ArrayInitParser(tokens);
            // Begin parsing at init rule
            IParseTree tree = parser.init();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            //  Look in obj/Debug/net6.0/ExprParser.cs
            // public ProgContext prog()
        }

        private static void Translate(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // create a CharStream that reads from standard input
            var input = new AntlrInputStream(text_reader);
            // create a lexer that feeds off of input CharStream
            ArrayInitLexer lexer = new ArrayInitLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            ArrayInitParser parser = new ArrayInitParser(tokens);
            IParseTree tree = parser.init(); // begin parsing at init rule

            // Create a generic parse tree walker that can trigger callbacks
            ParseTreeWalker walker = new ParseTreeWalker();
            // Walk the tree created during the parse, trigger callbacks
            walker.Walk(new ShortToUnicodeString(), tree);
            Console.WriteLine(); // print a \n after translation
        }

    }
}
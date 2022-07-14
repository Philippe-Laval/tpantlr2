using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("E1.input\n");

            Test("E1.input");

            Console.WriteLine("E2.input\n");

            Test("E2.input");

            Console.WriteLine("E3.input\n"); 
            
            Test("E3.input");

            Console.WriteLine("E4.input\n"); 
            
            Test("E4.input");

            Console.WriteLine("E5.input\n");

            Test("E5.input");
        }

        static void Test(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            SimpleLexer lexer = new SimpleLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            SimpleParser parser = new SimpleParser(tokens);

            parser.RemoveErrorListeners(); // remove ConsoleErrorListener
            parser.AddErrorListener(new VerboseListener()); // add ours
            parser.AddErrorListener(new UnderlineListener()); // add ours

            // To hear about it when the parser detects an ambiguity, tell the parser to use an instance of DiagnosticErrorListener using addErrorListener().
            parser.AddErrorListener(new DiagnosticErrorListener());

            // Begin parsing at rule prog
            IParseTree tree = parser.prog();

            // Stack [prog, classDef] indicates that the parser is in rule classDef, which was called by prog.
            // For example, token [@2,8:8='T',<9>,1:8] indicates that it is the third token in the token stream(index 2 from 0), ranges
            // from characters 8 to 8, has token type 9, resides on line 1, and is at character position 8 (counting from 0 in treating tabs as one character).

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
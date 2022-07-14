using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;

namespace TestCppStat
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Test("input1");
        }

        private static void Test(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            CppStatLexer lexer = new CppStatLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            CppStatParser parser = new CppStatParser(tokens);

            //parser.RemoveErrorListeners(); // remove ConsoleErrorListener
            //parser.AddErrorListener(new UnderlineListener()); // add ours
            parser.AddErrorListener(new DiagnosticErrorListener());

            // Here's how to make the parser report all ambiguities
            //parser.Interpreter.PredictionMode = PredictionMode.Sll;
            parser.Interpreter.PredictionMode = PredictionMode.LlExactAmbigDetection;

            // Begin parsing at stat rule
            IParseTree tree = parser.stat();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;


using System.Collections.Generic;

namespace TestPredCppStat
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("input1");
            Test("input2");
        }

        private static void Test(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            PredCppStatLexer lexer = new PredCppStatLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            PredCppStatParser parser = new PredCppStatParser(tokens);

            //parser.RemoveErrorListeners(); // remove ConsoleErrorListener
            //parser.AddErrorListener(new UnderlineListener()); // add ours
            parser.AddErrorListener(new DiagnosticErrorListener());

            // Here's how to make the parser report all ambiguities
            //parser.Interpreter.PredictionMode = PredictionMode.SLL;
            parser.Interpreter.PredictionMode = PredictionMode.LL_EXACT_AMBIG_DETECTION;

            // Begin parsing at stat rule
            IParseTree tree = parser.stat();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }
    }
}
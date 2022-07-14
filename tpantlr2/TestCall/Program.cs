using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using System.Runtime.InteropServices;

namespace TestCall
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("E.input");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            CallLexer lexer = new CallLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            CallParser parser = new CallParser(tokens);

            parser.RemoveErrorListeners(); // remove ConsoleErrorListener
            parser.AddErrorListener(new VerboseListener()); // add ours
            // To hear about it when the parser detects an ambiguity, tell the parser to use an instance of DiagnosticErrorListener using addErrorListener().
            //parser.AddErrorListener(new DiagnosticErrorListener());

            // Here's how to make the parser report all ambiguities
            //parser.Interpreter.PredictionMode = PredictionMode.Sll;
            parser.Interpreter.PredictionMode = PredictionMode.LlExactAmbigDetection;

            // Begin parsing at rule stat
            IParseTree tree = parser.stat();

            // Stack [prog, classDef] indicates that the parser is in rule classDef, which was called by prog.
            // For example, token [@2,8:8='T',<9>,1:8] indicates that it is the third token in the token stream(index 2 from 0), ranges
            // from characters 8 to 8, has token type 9, resides on line 1, and is at character position 8 (counting from 0 in treating tabs as one character).

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree
        }

    }
}
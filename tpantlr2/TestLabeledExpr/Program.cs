using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestLabeledExpr
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            LabeledExprLexer lexer = new LabeledExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            LabeledExprParser parser = new LabeledExprParser(tokens);
            // Begin parsing at rule prog
            IParseTree tree = parser.prog();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // Look in obj/Debug/net6.0/HelloParser.cs
            // public RContext r()

           EvalVisitor eval = new EvalVisitor();
           eval.Visit(tree);
        }
    }
}
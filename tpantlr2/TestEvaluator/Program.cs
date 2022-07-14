using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expr.g4");
            
            TestListener();
            TestListener2();

            TestExprEmitter();

            Console.WriteLine("LExpr.g4");

            TestVisitor();
            TestListener3();
        }

        static void TestListener()
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from text_reader
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            ExprLexer lexer = new ExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            ExprParser parser = new ExprParser(tokens);

            // Begin parsing at rule s
            IParseTree tree = parser.s();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // create a standard ANTLR parse tree walker
            ParseTreeWalker walker = new ParseTreeWalker();
            // create listener then feed to walker
            ExprListener listener = new ExprListener();
            // walk parse tree
            walker.Walk(listener, tree);

            // Dump the Values
            foreach (var t in listener.Values)
            {
                Console.WriteLine($"{t.Key}={t.Value}");
            }
        }

        static void TestVisitor()
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from text_reader
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            LExprLexer lexer = new LExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            LExprParser parser = new LExprParser(tokens);
            // tell ANTLR to build a parse tree
            parser.BuildParseTree = true;

            // Begin parsing at rule s
            IParseTree tree = parser.s();

            // print LISP-style tree
            Console.WriteLine(tree.ToStringTree(parser));

            EvalVisitor evalVisitor = new EvalVisitor();
            int result = evalVisitor.Visit(tree);
            Console.WriteLine("visitor result = " + result);
        }

        static void TestListener2()
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from text_reader
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            ExprLexer lexer = new ExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            ExprParser parser = new ExprParser(tokens);

            // Begin parsing at rule s
            IParseTree tree = parser.s();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // create a standard ANTLR parse tree walker
            ParseTreeWalker walker = new ParseTreeWalker();

            // create listener then feed to walker
            Evaluator listener = new Evaluator();
            // walk parse tree
            walker.Walk(listener, tree);
            Console.WriteLine("Stack Evaluator result = " + listener.Stack.Pop());

            EvaluatorWithProps eval2 = new EvaluatorWithProps();
            walker.Walk(eval2, tree);
            Console.WriteLine("result with tree props = " + eval2.Values.Get(tree));
        }

        static void TestListener3()
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from text_reader
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            LExprLexer lexer = new LExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            LExprParser parser = new LExprParser(tokens);
            parser.BuildParseTree = true;      // tell ANTLR to build a parse tree

            // Begin parsing at rule s
            IParseTree tree = parser.s();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // create a standard ANTLR parse tree walker
            ParseTreeWalker walker = new ParseTreeWalker();

            // create listener then feed to walker
            LEvaluatorWithProps evalProp = new LEvaluatorWithProps();
            walker.Walk(evalProp, tree);
            Console.WriteLine("result with tree props = " + evalProp.GetValue(tree));
        }
    
        static void TestExprEmitter()
        {
            using TextReader text_reader = File.OpenText("t.expr");

            // Create an input character stream from text_reader
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            ExprLexer lexer = new ExprLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            ExprParser parser = new ExprParser(tokens);

            // Begin parsing at rule s
            IParseTree tree = parser.s();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            // create a standard ANTLR parse tree walker
            ParseTreeWalker walker = new ParseTreeWalker();

            // create listener then feed to walker
            LeafListener listener = new LeafListener();
            // walk parse tree
            walker.Walk(listener, tree);

            Printer printer = new Printer();
            walker.Walk(printer, tree);
            Console.WriteLine();
        }

    }
}
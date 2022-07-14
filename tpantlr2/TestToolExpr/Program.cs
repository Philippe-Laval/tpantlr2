using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

// Moreover, we’ll employ a little trick to make it interactive,
// meaning we get results when we hit Return, not at the end of the input.
// Our examples so far have scarfed up the entire input and then processed the resulting parse trees.

namespace TestToolExpr
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">"t.expr" to read the file or we will need to enter the text manualy in the Console</param>
        static void Main(string[] args)
        {
            string? inputFile = null;   
            if (args.Length > 0) inputFile = args[0];

            TextReader text_reader = Console.In;
            if (inputFile != null)
            {
                text_reader = File.OpenText(inputFile);
            }

            string? expr = text_reader.ReadLine();       // get first expression
            int line = 1;                                // track input expr line numbers

            ExprParser parser = new ExprParser(null);   // share single parser instance
            parser.BuildParseTree = false;              // don't need trees

            while (expr != null)
            {             // while we have more expressions
                          // create new lexer and token stream for each line (expression)
                AntlrInputStream input = new AntlrInputStream(expr + "\n");
                ExprLexer lexer = new ExprLexer(input);
                lexer.Line = line;           // notify lexer of input position
                lexer.Column = 0;
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                parser.SetInputStream(tokens); // notify parser of new token stream
                parser.stat();                 // start the parser
                expr = text_reader.ReadLine();          // see if there's another line
                line++;
            }

            if (inputFile != null)
            {
                text_reader.Close();
            }
        }

    }
}
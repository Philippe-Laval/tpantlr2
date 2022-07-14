using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestRows
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.rows");

            int col = int.Parse(args[0]);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            RowsLexer lexer = new RowsLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            // pass column number
            RowsParser parser = new RowsParser(tokens, col); 
            // don't waste time bulding a tree
            parser.BuildParseTree = false;
            // Begin parsing at rule file
            IParseTree tree = parser.file();
        }
    }
}
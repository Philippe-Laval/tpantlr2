using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestXMLLexer
{
    class Program
    {
        static void Main(string[] args)
        {
            using TextReader text_reader = File.OpenText("t.xml");

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            XMLLexer lexer = new XMLLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            //lexer.EmitEOF();

            //string result = tokens.GetText();
            IList<IToken> tokenList = lexer.GetAllTokens();
            foreach(IToken token in tokenList)
            {
                string text = token.Text;
                text = text.Replace("\n", "\\n");
                text = text.Replace("\r", "\\r");
                text = text.Replace("\t", "\\t");

                Console.WriteLine($"TokenIndex:{token.TokenIndex} StartIndex/StopIndex:{token.StartIndex}:{token.StopIndex} Text:'{text}' Type:{token.Type} Line:{token.Line} Column:{token.Column} Channel:{token.Channel}");
            }

        }
    }
}
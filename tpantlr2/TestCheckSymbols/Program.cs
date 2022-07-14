using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestCheckSymbols
{
    class Program
    {
        public static Symbol.SymbolType GetType(int tokenType)
        {
            switch (tokenType)
            {
                case CymbolParser.K_VOID: return Symbol.SymbolType.tVOID;
                case CymbolParser.K_INT: return Symbol.SymbolType.tINT;
                case CymbolParser.K_FLOAT: return Symbol.SymbolType.tFLOAT;
            }
            return Symbol.SymbolType.tINVALID;
        }

        public static void Error(IToken t, String msg)
        {
            Console.WriteLine($"line {t.Line}:{t.Column} {msg}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("vars.cymbol");

            Test("vars.cymbol");

            Console.WriteLine("vars2.cymbol");

            Test("vars2.cymbol");
        }

        static void Test(string fileName)
        {
            using TextReader text_reader = File.OpenText(fileName);

            // Create an input character stream from standard in
            var input = new AntlrInputStream(text_reader);
            // Create an ExprLexer that feeds from that stream
            CymbolLexer lexer = new CymbolLexer(input);
            // Create a stream of tokens fed by the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // Create a parser that feeds off the token stream
            CymbolParser parser = new CymbolParser(tokens);
            parser.BuildParseTree = true;

            // Begin parsing at rule file
            IParseTree tree = parser.file();

            Console.WriteLine(tree.ToStringTree(parser)); // print LISP-style tree

            ParseTreeWalker walker = new ParseTreeWalker();

            DefPhase defPhase = new DefPhase();
            walker.Walk(defPhase, tree);
            
            // create next phase and feed symbol table info from def to ref phase
            RefPhase refPhase = new RefPhase(defPhase.globals, defPhase.scopes);
            walker.Walk(refPhase, tree);
        }
    }
}







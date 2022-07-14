using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace TestExtractInterface
{
    public class ExtractInterfaceListener : JavaBaseListener
    {
        JavaParser parser;

        public ExtractInterfaceListener(JavaParser parser)
        {
            this.parser = parser;
        }
        /** Listen to matches of classDeclaration */

        public override void EnterClassDeclaration(JavaParser.ClassDeclarationContext ctx)
        {
            Console.WriteLine("interface I" + ctx.Identifier() + " {");
        }

        public override void ExitClassDeclaration(JavaParser.ClassDeclarationContext ctx)
        {
            Console.WriteLine("}");
        }

        /** Listen to matches of methodDeclaration */

        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext ctx)
        {
            // need parser to get tokens
            // C# equivalent of java : TokenStream tokens = parser.getTokenStream();
            ITokenStream tokens = (ITokenStream)parser.InputStream;

            string type = "void";
            if (ctx.type() != null)
            {
                type = tokens.GetText(ctx.type());
            }
            string args = tokens.GetText(ctx.formalParameters());
            Console.WriteLine("\t" + type + " " + ctx.Identifier() + args + ";");
        }


        /// <summary>
        /// simply print the text matched by the entire rule
        /// </summary>
        /// <param name="ctx"></param>
        public override void EnterImportDeclaration(JavaParser.ImportDeclarationContext ctx)
        {
            // need parser to get tokens
            // C# equivalent of java : TokenStream tokens = parser.getTokenStream();
            ITokenStream tokens = (ITokenStream)parser.InputStream;

            string result = tokens.GetText(ctx);
            Console.WriteLine(result);
        }

    }
}

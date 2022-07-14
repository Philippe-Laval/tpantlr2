using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInsertSerialID
{
    public class InsertSerialIDListener : JavaBaseListener
    {
        public TokenStreamRewriter Rewriter { get; protected set; }

        public InsertSerialIDListener(ITokenStream tokens)
        {
            Rewriter = new TokenStreamRewriter(tokens);
        }

        public override void EnterClassBody(JavaParser.ClassBodyContext ctx)
        {
            String field = "\n\tpublic static final long serialVersionUID = 1L;";
            Rewriter.InsertAfter(ctx.start, field);
        }
    }
}

using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluator
{
    /// <summary>
    /// Sample "emitter" that relies on order of enter/exit.
    /// 1+2*3 => (1+(2*3))
    /// </summary>
    public class Printer : ExprBaseListener
    {
        public override void EnterE(ExprParser.EContext ctx)
        {
            if (ctx.ChildCount > 1) Console.Write("(");
        }

        public override void ExitE(ExprParser.EContext ctx)
        {
            if (ctx.ChildCount > 1) Console.Write(")");
        }

        public override void VisitTerminal(ITerminalNode node)
        {
            Console.Write(node.GetText());
        }

    }
}

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluator
{
    public class EvaluatorWithProps : ExprBaseListener
    {
        public ParseTreeProperty<int> Values { get; set; } = new ParseTreeProperty<int>();

        
        public override void ExitS(ExprParser.SContext ctx)
        {
            Values.Put(ctx, Values.Get(ctx.GetChild(0)));
        }

        public override void ExitE(ExprParser.EContext ctx)
        {
            if (ctx.ChildCount == 3)
            { 
                // operations have 3 children
                int left = Values.Get(ctx.e(0));
                int right = Values.Get(ctx.e(1));
                if (ctx.op.Type == ExprParser.MULT)
                {
                    Values.Put(ctx, left * right);
                }
                else
                {
                    Values.Put(ctx, left + right);
                }
            }
            else
            {
                // an INT
                Values.Put(ctx, Values.Get(ctx.GetChild(0))); 
            }
        }

        public override void VisitTerminal(ITerminalNode node)
        {
            IToken symbol = node.Symbol;
            if (symbol.Type == ExprParser.INT)
            {
                Values.Put(node, int.Parse(symbol.Text));
            }
        }


    }
}

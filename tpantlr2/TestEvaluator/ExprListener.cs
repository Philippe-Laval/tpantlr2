using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluator
{
    public class ExprListener : ExprBaseListener
    {
        public Dictionary<ExprParser.EContext, int> Values { get; set; } = new Dictionary<ExprParser.EContext, int>();

        public override void ExitE(ExprParser.EContext ctx)
        {
            if (ctx.ChildCount == 3)
            {
                // operations have 3 children
                int left = Values[ctx.e(0)];
                int right = Values[ctx.e(1)];
                if (ctx.op.Type == ExprParser.MULT)
                {
                    Values.Add(ctx, left * right);
                }
                else
                {
                    Values.Add(ctx, left + right);
                }
            }
            else
            {
                // an INT
                string valueToParse = ctx.INT().GetText();
                int value = int.Parse(valueToParse);
                Values.Add(ctx, value);
            }
        }

        public override void ExitS(ExprParser.SContext ctx)
        {
            int result = Values[ctx.e()];
            Console.WriteLine($"Result={result}");
        }

    }
}
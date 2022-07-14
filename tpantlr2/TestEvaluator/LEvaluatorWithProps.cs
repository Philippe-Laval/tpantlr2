using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestEvaluator
{
    /// <summary>
    /// Sample "calculator" using property of nodes
    /// </summary>
    public class LEvaluatorWithProps : LExprBaseListener
    {
        /** maps nodes to integers with Map<ParseTree,Integer> */
        private ParseTreeProperty<int> values = new ParseTreeProperty<int>();

        /** Need to pass e's value out of rule s : e ; */
        public override void ExitS(LExprParser.SContext ctx)
        {
            SetValue(ctx, GetValue(ctx.e())); // like: int s() { return e(); }
        }

        public override void ExitMult(LExprParser.MultContext ctx)
        {
            int left = GetValue(ctx.e(0));  // e '*' e   # Mult
            int right = GetValue(ctx.e(1));
            SetValue(ctx, left * right);
        }

        public override void ExitAdd(LExprParser.AddContext ctx)
        {
            int left = GetValue(ctx.e(0)); // e '+' e   # Add
            int right = GetValue(ctx.e(1));
            SetValue(ctx, left + right);
        }

        public override void ExitInt(LExprParser.IntContext ctx)
        {
            String intText = ctx.INT().GetText(); // INT   # Int
            SetValue(ctx, int.Parse(intText));
        }

        public void SetValue(IParseTree node, int value) 
        { 
            values.Put(node, value); 
        }
        public int GetValue(IParseTree node)
        { 
            return values.Get(node); 
        }

    }
}

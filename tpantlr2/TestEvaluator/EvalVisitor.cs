using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluator
{
    public class EvalVisitor : LExprBaseVisitor<int>
    {
        // Notice that EvalVisitor doesn’t have a visitor method for rule s. The default
        // implementation of visitS() in LExprBaseVisitor calls predefined method ParseTreeVisitor.visitChildren().
        // visitChildren() returns the value returned from the visit of the last child.
        // In this case, visitS() returns the value of the expression returned from visiting its only child (the e node).
        // We can use this default behavior.

        public override int VisitMult(LExprParser.MultContext ctx)
        {
            return Visit(ctx.e(0)) * Visit(ctx.e(1));
        }

        public override int VisitAdd(LExprParser.AddContext ctx)
        {
            return Visit(ctx.e(0)) + Visit(ctx.e(1));
        }

        public override int VisitInt(LExprParser.IntContext ctx)
        {
            return int.Parse(ctx.INT().GetText());
        }
    }
}


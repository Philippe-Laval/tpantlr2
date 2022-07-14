using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLabeledExpr
{
    public class EvalVisitor : LabeledExprBaseVisitor<int>
    {
        /// <summary>
        /// "memory" for our calculator; variable/value pairs go here
        /// </summary>
        Dictionary<String, int> memory = new Dictionary<String, int>();

        /// <summary>
        /// ID '=' expr NEWLINE
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitAssign(LabeledExprParser.AssignContext ctx)
        {
            String id = ctx.ID().GetText();  // id is left-hand side of '='
            int value = Visit(ctx.expr());   // compute value of expression on right
            memory.Add(id, value);           // store it in our memory
            return value;
        }

        /// <summary>
        /// expr NEWLINE
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitPrintExpr(LabeledExprParser.PrintExprContext ctx)
        {
            int value = Visit(ctx.expr());  // evaluate the expr child
            Console.WriteLine(value);       // print the result
            return 0;                       // return dummy value
        }

        /// <summary>
        /// INT
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitInt(LabeledExprParser.IntContext ctx)
        {
            return int.Parse(ctx.INT().GetText());
        }

        /// <summary>
        /// ID
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitId(LabeledExprParser.IdContext ctx)
        {
            String id = ctx.ID().GetText();
            if (memory.ContainsKey(id)) return memory[id];
            return 0;
        }

        /// <summary>
        /// expr op=('*'|'/') expr
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitMulDiv(LabeledExprParser.MulDivContext ctx)
        {
            int left = Visit(ctx.expr(0));  // get value of left subexpression
            int right = Visit(ctx.expr(1)); // get value of right subexpression
            if (ctx.op.Type == LabeledExprParser.MUL) return left * right;
            return left / right; // must be DIV
        }

        /// <summary>
        /// expr op=('+'|'-') expr
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitAddSub(LabeledExprParser.AddSubContext ctx)
        {
            int left = Visit(ctx.expr(0));  // get value of left subexpression
            int right = Visit(ctx.expr(1)); // get value of right subexpression
            if (ctx.op.Type == LabeledExprParser.ADD) return left + right;
            return left - right; // must be SUB
        }

        /// <summary>
        /// '(' expr ')'
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override int VisitParens(LabeledExprParser.ParensContext ctx)
        {
            return Visit(ctx.expr()); // return child expr's value
        }
    }
}

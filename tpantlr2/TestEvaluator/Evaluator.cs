using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluator
{
    /// <summary>
    /// Sample "calculator" (special case of collector)
    /// </summary>
    public class Evaluator : ExprBaseListener
    {
        /// <summary>
        /// The idea is to push the result of computing a subexpression onto the stack.
        /// Methods for subexpressions further up the parse tree pop operands off the stack.
        /// </summary>
        public Stack<int> Stack { get; set; } = new Stack<int>();

        public override void ExitE(ExprParser.EContext ctx)
        {
            if (ctx.ChildCount == 3)
            {
                // operations have 3 children
                int right = Stack.Pop();
                int left = Stack.Pop();
                if (ctx.op.Type == ExprParser.MULT)
                {
                    Stack.Push(left * right);
                }
                else
                {
                    // must be add
                    Stack.Push(left + right);
                }
            }
        }

        public override void VisitTerminal(ITerminalNode node)
        {
            IToken symbol = node.Symbol;
            if (symbol.Type == ExprParser.INT)
            {
                Stack.Push(int.Parse(symbol.Text));
            }
        }
    }
}

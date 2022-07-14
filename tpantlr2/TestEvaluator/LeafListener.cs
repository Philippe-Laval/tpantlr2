using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluator
{
    /// <summary>
    /// Sample "emitter"
    /// </summary>
    public class LeafListener : ExprBaseListener
    {
        public override void VisitTerminal(ITerminalNode node)
        {
            Console.WriteLine(node.GetText());
        }
    
    }
}

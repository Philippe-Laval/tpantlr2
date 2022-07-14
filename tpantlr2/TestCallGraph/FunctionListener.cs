using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCallGraph
{
    public class FunctionListener : CymbolBaseListener
    {
        public Graph Graph { get; set; } = new Graph();
        private string? _currentFunctionName = null;

        public override void EnterFunctionDecl(CymbolParser.FunctionDeclContext ctx)
        {
            _currentFunctionName = ctx.ID().GetText();
            Graph.Nodes.Add(_currentFunctionName);
        }

        public override void ExitCall(CymbolParser.CallContext ctx)
        {
            String funcName = ctx.ID().GetText();
            // map current function to the callee
            Graph.Edge(_currentFunctionName!, funcName);
        }

    }
}

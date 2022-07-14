using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestCheckSymbols
{
    public class RefPhase : CymbolBaseListener
    {
        ParseTreeProperty<IScope> scopes;
        GlobalScope globals;
        IScope? currentScope = null; // resolve symbols starting in this scope

        public RefPhase(GlobalScope globals, ParseTreeProperty<IScope> scopes)
        {
            this.scopes = scopes;
            this.globals = globals;
        }
        public override void EnterFile(CymbolParser.FileContext ctx)
        {
            currentScope = globals;
        }

        public override void EnterFunctionDecl(CymbolParser.FunctionDeclContext ctx)
        {
            currentScope = scopes.Get(ctx);
        }
        public override void ExitFunctionDecl(CymbolParser.FunctionDeclContext ctx)
        {
            currentScope = currentScope?.GetEnclosingScope();
        }

        public override void EnterBlock(CymbolParser.BlockContext ctx)
        {
            currentScope = scopes.Get(ctx);
        }
        public override void ExitBlock(CymbolParser.BlockContext ctx)
        {
            currentScope = currentScope?.GetEnclosingScope();
        }

        public override void ExitVar(CymbolParser.VarContext ctx)
        {
            String name = ctx.ID().Symbol.Text;
            Symbol? variable = currentScope?.Resolve(name);
            if (variable == null)
            {
                Program.Error(ctx.ID().Symbol, "no such variable: " + name);
            }
            if (variable is FunctionSymbol ) {
                Program.Error(ctx.ID().Symbol, name + " is not a variable");
            }
        }

        public override void ExitCall(CymbolParser.CallContext ctx)
        {
            // can only handle f(...) not expr(...)
            String funcName = ctx.ID().GetText();
            Symbol? meth = currentScope?.Resolve(funcName);
            if (meth == null)
            {
                Program.Error(ctx.ID().Symbol, "no such function: " + funcName);
            }
            if (meth is VariableSymbol ) {
                Program.Error(ctx.ID().Symbol, funcName + " is not a function");
            }
        }
    
    }
}

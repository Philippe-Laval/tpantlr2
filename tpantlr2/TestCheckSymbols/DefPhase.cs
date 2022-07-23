using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TestCheckSymbols
{
    public class DefPhase : CymbolBaseListener
    {
        public ParseTreeProperty<IScope> scopes { get; set; } = new ParseTreeProperty<IScope>();
        public GlobalScope globals { get; set; } = null!;
        private IScope? currentScope = null;     // define symbols in this scope

        public override void EnterFile(CymbolParser.FileContext ctx)
        {
            globals = new GlobalScope(null);
            currentScope = globals;
        }

        public override void ExitFile(CymbolParser.FileContext ctx)
        {
            Console.WriteLine(globals.ToString());
        }

        public override void EnterFunctionDecl(CymbolParser.FunctionDeclContext ctx)
        {
            String name = ctx.ID().GetText();
            int typeTokenType = ctx.type().Start.Type;
            Symbol.SymbolType type = Program.GetType(typeTokenType);

            // push new scope by making new one that points to enclosing scope
            FunctionSymbol function = new FunctionSymbol(name, type, currentScope);
            currentScope?.Define(function); // Define function in current scope
            SaveScope(ctx, function);      // Push: set function's parent to current
            currentScope = function;       // Current scope is now function scope
        }

        private void SaveScope(ParserRuleContext ctx, IScope s)
        {
            scopes.Put(ctx, s); 
        }

        public override void ExitFunctionDecl(CymbolParser.FunctionDeclContext ctx)
        {
            Console.WriteLine(currentScope?.ToString());
            currentScope = currentScope?.GetEnclosingScope(); // pop scope
        }

        public override void EnterBlock(CymbolParser.BlockContext ctx)
        {
            // push new local scope
            currentScope = new LocalScope(currentScope);
            SaveScope(ctx, currentScope);
        }

        public override void ExitBlock(CymbolParser.BlockContext ctx)
        {
            Console.WriteLine(currentScope);
            currentScope = currentScope?.GetEnclosingScope(); // pop scope
        }

        public override void ExitFormalParameter(CymbolParser.FormalParameterContext ctx)
        {
            DefineVar(ctx.type(), ctx.ID().Symbol);
        }

        public override void ExitVarDecl(CymbolParser.VarDeclContext ctx)
        {
            DefineVar(ctx.type(), ctx.ID().Symbol);
        }

        private void DefineVar(CymbolParser.TypeContext typeCtx, IToken nameToken)
        {
            int typeTokenType = typeCtx.Start.Type;
            Symbol.SymbolType type = Program.GetType(typeTokenType);
            VariableSymbol var = new VariableSymbol(nameToken.Text, type);
            currentScope?.Define(var); // Define symbol in current scope
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckSymbols
{
    /// <summary>
    /// FunctionSymbol objects do double duty as a symbol and as the scope containing the arguments.
    /// </summary>
    public class FunctionSymbol : Symbol, IScope
    {
        Dictionary<string, Symbol> arguments = new Dictionary<string, Symbol>();
        IScope? enclosingScope = null;

        public FunctionSymbol(string name, SymbolType retType, IScope? enclosingScope) 
            : base(name, retType)
        {
            this.enclosingScope = enclosingScope;
        }

        public Symbol? Resolve(String name)
        {
            Symbol? s = null;

            if (arguments.ContainsKey(name))
            {
                s = arguments[name];
            }

            if (s != null) return s;
            
            // if not here, check any enclosing scope
            var scope = GetEnclosingScope();
            if (scope != null)
            {
                return scope.Resolve(name);
            }

            // not found
            return null; 
        }

        public void Define(Symbol sym)
        {
            arguments.Add(sym.Name, sym);
            sym.Scope = this; // track the scope in each symbol
        }

        public IScope? GetEnclosingScope() 
        {
            return enclosingScope;
        }

        public String GetScopeName() 
        { 
            return Name; 
        }

        public override string ToString()
        {
            var temp = arguments.Select(o => $"{o.Value.ToString()}").ToList();
            var paramResult = string.Join(", ", temp);
            string result = $"function{base.ToString()}:[{paramResult}]";
            return result;
        }
    }
}


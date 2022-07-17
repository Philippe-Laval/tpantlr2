using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckSymbols
{
    public abstract class BaseScope : IScope
    {
        /// <summary>
        /// null if global (outermost) scope
        /// </summary>
        IScope? EnclosingScope = null;
        Dictionary<string, Symbol> Symbols = new Dictionary<string, Symbol>();

        public BaseScope(IScope? enclosingScope)
        { 
            this.EnclosingScope = enclosingScope;
        }

        public Symbol? Resolve(String name)
        {
            Symbol? s = null;
            if (Symbols.ContainsKey(name))
            {
                s = Symbols[name];
            }
            
            if (s != null) return s;

            // if not here, check any enclosing scope
            if (EnclosingScope != null) return EnclosingScope.Resolve(name);
            
            return null; // not found
        }

        public void Define(Symbol sym)
        {
            Symbols.Add(sym.Name, sym);
            sym.Scope = this; // track the scope in each symbol
        }

        public IScope? GetEnclosingScope() { return EnclosingScope; }

        public override string ToString()
        {
            var temp = Symbols.Select(o => $"{o.Value.ToString()}").ToList();
            var result = String.Join(", ", temp);

            return $"{GetScopeName()}:[{result}]";          
        }

        public virtual string GetScopeName()
        {
            return "BaseScope";
        }
    }
}

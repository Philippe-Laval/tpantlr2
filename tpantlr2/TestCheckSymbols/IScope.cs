using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckSymbols
{
    public interface IScope
    {
        public string GetScopeName();

        /// <summary>
        /// Where to look next for symbols
        /// </summary>
        /// <returns></returns>
        public IScope? GetEnclosingScope();

        /// <summary>
        /// Define a symbol in the current scope
        /// </summary>
        /// <param name="sym"></param>
        public void Define(Symbol sym);

        /// <summary>
        /// Look up name in this scope or in enclosing scope if not here
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Symbol? Resolve(string name);
    }
}

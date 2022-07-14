using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckSymbols
{
    /// <summary>
    /// Represents a variable definition (name,type) in symbol table
    /// </summary>
    public class VariableSymbol : Symbol
    {
        public VariableSymbol(string name, SymbolType type) 
            : base(name, type) 
        {
        }
    }
}

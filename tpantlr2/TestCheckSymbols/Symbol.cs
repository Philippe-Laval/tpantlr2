using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckSymbols
{
    /// <summary>
    /// A generic programming language symbol
    /// </summary>
    public class Symbol
    {
        public enum SymbolType { tINVALID, tVOID, tINT, tFLOAT }

        /// <summary>
        /// All symbols at least have a name
        /// </summary>
        public String Name { get; set; } = String.Empty;
        public SymbolType Type { get; set; } = SymbolType.tINVALID;
        /// <summary>
        /// All symbols know what scope contains them.
        /// </summary>
        public IScope? Scope { get; set; } = null; 

        public Symbol(String name) 
        {
            this.Name = name; 
        }

        public Symbol(String name, SymbolType type) 
            : this(name)
        {
            this.Type = type;
        }
        
        public String GetName() 
        { 
            return Name;
        }

        public override string ToString()
        {
            if (Type != SymbolType.tINVALID) return '<' + GetName() + ":" + Type + '>';
            return GetName();
        }
    }
}

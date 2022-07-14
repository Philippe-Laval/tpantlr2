using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckSymbols
{
    public class GlobalScope : BaseScope
    {
        public GlobalScope(IScope? enclosingScope) 
            : base(enclosingScope) 
        {  
        }

        public override string GetScopeName()
        { 
            return "globals"; 
        }
    
    }
}

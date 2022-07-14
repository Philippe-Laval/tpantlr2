using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPropertyFileCoupled
{
    public class PropertyFilePrinterParser : PropertyFileCoupledParser
    {
        public PropertyFilePrinterParser(ITokenStream input) 
            : base(input)
        {
        }

        public override void DefineProperty(IToken name, IToken value)
        {
            Console.WriteLine(name.Text + "=" + value.Text);
        }

    }
}

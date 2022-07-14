using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPropertyFileCoupled
{
    public class PropertyFileLoaderParser : PropertyFileCoupledParser
    {
        public Dictionary<string, string> Props { get; set; } = new Dictionary<string, string>();

        public PropertyFileLoaderParser(ITokenStream input)
            : base(input)
        {
        }

        public override void DefineProperty(IToken name, IToken value)
        {
            Props.Add(name.Text, value.Text);
        }
    }
}

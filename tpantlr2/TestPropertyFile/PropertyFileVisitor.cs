using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPropertyFile
{
    public class PropertyFileVisitor : PropertyFileBaseVisitor<int>
    {
        public Dictionary<string, string> Props { get; set; } = new Dictionary<string, string>();

        public override int VisitProp(PropertyFileParser.PropContext ctx)
        {
            // In this case, the nodes created for prop invocations don't have children,
            // so VisitProp() doesn’t have to call Visit().

            // prop : ID '=' STRING '\n' ;
            String id = ctx.ID().GetText();
            String value = ctx.STRING().GetText();
            Props.Add(id, value);
            return 0;
        }
    }
}
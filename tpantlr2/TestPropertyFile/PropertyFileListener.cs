using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPropertyFile
{
    public class PropertyFileListener : PropertyFileBaseListener
    {
        public Dictionary<string, string> Props { get; set; } = new Dictionary<string, string>();

        public override void ExitProp(PropertyFileParser.PropContext ctx)
        {
            // prop : ID '=' STRING '\n' ;
            string id = ctx.ID().GetText(); 
            string value = ctx.STRING().GetText();
            Props.Add(id, value);
        }
    }
}
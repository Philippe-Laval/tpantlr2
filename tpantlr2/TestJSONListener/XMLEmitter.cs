using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
{
    "description" : "An imaginary server config file",
    "logs" : {"level":"verbose", "dir":"/var/log"},
    "host" : "antlr.org",
    "admin": ["parrt", "tombu"]
    "aliases": []
}

to

<description>An imaginary server config file</description>
<logs>
    <level>verbose</level>
    <dir>/var/log</dir>
</logs>
<host>antlr.org</host>
<admin>
    <element>parrt</element> <!-- inexact -->
    <element>tombu</element>
</admin>
<aliases></aliases>
 */

namespace TestJSONListener
{
    public class XMLEmitter : JSONBaseListener
    {
        private ParseTreeProperty<String> _xml = new ParseTreeProperty<String>();

        public string GetXML(IParseTree ctx) { return _xml.Get(ctx); }
        public void SetXML(IParseTree ctx, string s) { _xml.Put(ctx, s); }

        public override void ExitJson(JSONParser.JsonContext ctx)
        {
            SetXML(ctx, GetXML(ctx.GetChild(0)));
        }

        public override void ExitAnObject(JSONParser.AnObjectContext ctx)
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("\n");
            foreach (JSONParser.PairContext pctx in ctx.pair())
            {
                buf.Append(GetXML(pctx));
            }
            SetXML(ctx, buf.ToString());
        }

        public override void ExitEmptyObject(JSONParser.EmptyObjectContext ctx)
        {
            SetXML(ctx, "");
        }

        public override void ExitArrayOfValues(JSONParser.ArrayOfValuesContext ctx)
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("\n");
            foreach (JSONParser.ValueContext vctx in ctx.value())
            {
                buf.Append("<element>"); // conjure up element for valid XML
                buf.Append(GetXML(vctx));
                buf.Append("</element>");
                buf.Append("\n");
            }
            SetXML(ctx, buf.ToString());
        }

        public override void ExitEmptyArray(JSONParser.EmptyArrayContext ctx)
        {
            SetXML(ctx, "");
        }

        public override void ExitPair(JSONParser.PairContext ctx)
        {
            String tag = StripQuotes(ctx.STRING().GetText());
            JSONParser.ValueContext vctx = ctx.value();
            String x = $"<{tag}>{GetXML(vctx)}</{tag}>\n";
            SetXML(ctx, x);
        }

        public override void ExitObjectValue(JSONParser.ObjectValueContext ctx)
        {
            // analogous to string value() {return @object();}
            SetXML(ctx, GetXML(ctx.@object()));
        }

        public override void ExitArrayValue(JSONParser.ArrayValueContext ctx)
        {
            SetXML(ctx, GetXML(ctx.array())); // string value() {return array();}
        }

        public override void ExitAtom(JSONParser.AtomContext ctx)
        {
            SetXML(ctx, ctx.GetText());
        }

        public override void ExitString(JSONParser.StringContext ctx)
        {
            SetXML(ctx, StripQuotes(ctx.GetText()));
        }

        public static String StripQuotes(String s)
        {
            //if (s == null || s[0] != '"') return s;
            //return s.Substring(1, s.Length - 2);

            return s.Trim('"');
        }
    }
}

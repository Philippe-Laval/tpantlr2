using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSVListener
{
    public class Loader : CSVBaseListener
    {
        public const string EMPTY = "";
        /// <summary>
        /// Load a list of row maps that map field name to value
        /// </summary>
        public List<Dictionary<string, string>> Rows { get; set; } = new List<Dictionary<string, string>>();
        /// <summary>
        /// List of column names
        /// </summary>
        public List<string> Header { get; set; } = new List<String>();
        /// <summary>
        /// Build up a list of fields in current row
        /// </summary>
        private List<string> _currentRowFieldValues = new List<String>();

        public override void ExitHdr(CSVParser.HdrContext ctx)
        {
            Header.AddRange(_currentRowFieldValues);
        }

        public override void EnterRow(CSVParser.RowContext ctx)
        {
            _currentRowFieldValues = new List<String>();
        }

        public override void ExitRow(CSVParser.RowContext ctx)
        {
            // If this is the header row, do nothing
            // if ( ctx.Parent is CSVParser.HdrContext ) return; OR:
            if (ctx.Parent.RuleIndex == CSVParser.RULE_hdr) return;
            // It's a data row
            Dictionary<string, string> m = new Dictionary<string, string>();
            int i = 0;
            foreach (string v in _currentRowFieldValues)
            {
                m.Add(Header[i], v);
                i++;
            }
            Rows.Add(m);
        }

        public override void ExitString(CSVParser.StringContext ctx)
        {
            _currentRowFieldValues.Add(ctx.STRING().GetText());
        }

        public override void ExitText(CSVParser.TextContext ctx)
        {
            _currentRowFieldValues.Add(ctx.TEXT().GetText());
        }

        public override void ExitEmpty(CSVParser.EmptyContext ctx)
        {
            _currentRowFieldValues.Add(EMPTY);
        }

    }
}

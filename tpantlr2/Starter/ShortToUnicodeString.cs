using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter
{
    public class ShortToUnicodeString : ArrayInitBaseListener
    {
        /// <summary>
        /// Translate { to "
        /// </summary>
        /// <param name="ctx"></param>
        public override void EnterInit(ArrayInitParser.InitContext ctx)
        {
            Console.Write('"');
        }

        /// <summary>
        /// Translate } to "
        /// </summary>
        /// <param name="ctx"></param>
        public override void ExitInit(ArrayInitParser.InitContext ctx)
        {
            Console.Write('"');
        }

        /// <summary>
        /// Translate integers to 4-digit hexadecimal strings prefixed with \\u
        /// </summary>
        /// <param name="ctx"></param>
        public override void EnterValue(ArrayInitParser.ValueContext ctx)
        {
            // Assumes no nested array initializers
            int value = int.Parse(ctx.INT().GetText());
            Console.Write($"\\u{value:x4}");
        }
    }
   
}

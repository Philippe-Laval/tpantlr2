using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBail
{
    public class MyBailErrorStrategy : DefaultErrorStrategy
    {
        /// <summary>
        /// Instead of recovering from exception e, rethrow it wrapped
        /// in a generic SystemException so it is not caught by the
        /// rule function catches.Exception e is the "cause" of the
        /// SystemException.
        /// </summary>
        /// <param name="recognizer"></param>
        /// <param name="e"></param>
        /// <exception cref="SystemException"></exception>
        public override void Recover(Parser recognizer, RecognitionException e)
        {
            throw new SystemException("Recover", e);
        }

        /// <summary>
        /// Make sure we don't attempt to recover inline; if the parser
        /// successfully recovers, it won't throw an exception.
        /// </summary>
        /// <param name="recognizer"></param>
        /// <returns></returns>
        /// <exception cref="SystemException"></exception>
        public override IToken RecoverInline(Parser recognizer)
        {
            throw new SystemException("RecoverInline", new InputMismatchException(recognizer));
        }


        /// <summary>
        /// Make sure we don't attempt to recover from problems in subrules.
        /// </summary>
        /// <param name="recognizer"></param>
        public override void Sync(Parser recognizer)
        { 
        }
    }
}

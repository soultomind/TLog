using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TLog
{
    public class ThreadPatternLayout : PatternLayout
    {
        public ThreadPatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(object obj = null)
        {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }
}

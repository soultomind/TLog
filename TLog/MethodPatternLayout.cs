using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class MethodPatternLayout : PatternLayout
    {
        public MethodPatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(object obj = null)
        {
            StackFrame sf = obj as StackFrame;
            return sf.GetMethod().Name;
        }
    }
}

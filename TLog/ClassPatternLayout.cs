using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class ClassPatternLayout : PatternLayoutType
    {
        public ClassPatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(object obj)
        {
            StackFrame sf = obj as StackFrame;
            return sf.GetMethod().ReflectedType.Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class IncludeFilterPatternLayout : PatternLayoutType
    {
        public string IncludeFilter { get; set; }
        public IncludeFilterPatternLayout(string typeString) 
            : base(typeString)
        {
        }

        public override string ConvertArgument(object obj = null)
        {
            if (IncludeFilter == null)
            {
                IncludeFilter = Assembly.GetAssembly(typeof(IncludeFilterPatternLayout)).GetName().Name;
            }
            return IncludeFilter;
        }
    }
}

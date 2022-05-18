using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class DatePatternLayout : PatternLayoutType
    {
        public DatePatternLayout(string typeString) 
            : base(typeString)
        {
        }

        public override string ConvertArgument(object obj = null)
        {
            string format = obj as string;
            return DateTime.Now.ToString(format);
        }
    }
}

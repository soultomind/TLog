using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class DatePatternLayout : PatternLayoutType
    {
        public string Format { get; set; }
        public DatePatternLayout(string typeString) 
            : base(typeString)
        {
        }

        public override string ConvertArgument(object obj = null)
        {
            return DateTime.Now.ToString(Format);
        }
    }
}

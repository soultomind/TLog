using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class DatePatternLayout : PatternLayoutType
    {
        public string Format
        {
            get { return _format; }
            set
            {
                DateTime.Now.ToString(value);
                _format = value;
            }
        }
        private string _format;

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

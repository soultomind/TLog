using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class IncludeFilterPatternLayout : PatternLayout
    {
        public string IncludeFilter
        {
            get { return _includeFilter; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }

                _includeFilter = value;
            }
        }
        private string _includeFilter;
        public IncludeFilterPatternLayout(string typeString) 
            : base(typeString)
        {
        }

        public override string ConvertArgument(IFormatMessage formatMessage)
        {
            return IncludeFilter;
        }
    }
}

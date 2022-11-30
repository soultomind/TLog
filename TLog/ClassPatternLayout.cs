using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class ClassPatternLayout : PatternLayout
    {
        public bool IsFullName
        {
            get { return _isFullName; }
            set { _isFullName = value; }
        }
        private bool _isFullName = true;
        public ClassPatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(IFormatMessage formatMessage)
        {
            StackFrame sf = formatMessage.StackFrame;
            if (_isFullName)
            {
                return sf.GetMethod().ReflectedType.FullName;
            }
            else
            {
                return sf.GetMethod().ReflectedType.Name;
            }
        }
    }
}

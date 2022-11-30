using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class NewLinePatternLayout : PatternLayout
    {
        public NewLinePatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(IFormatMessage formatMessage)
        {
            return Environment.NewLine;
        }
    }
}

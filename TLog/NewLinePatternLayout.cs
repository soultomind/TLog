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

        public override string ConvertArgument(object obj = null)
        {
            return Environment.NewLine;
        }
    }
}

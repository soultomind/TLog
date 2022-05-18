using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class LevelPatternLayout : PatternLayoutType
    {
        public LevelPatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(object obj = null)
        {
            LogLevel logLevel = obj as LogLevel;
            return logLevel.StrLevel.ToUpper();
        }
    }
}

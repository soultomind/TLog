using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class LevelPatternLayout : PatternLayout
    {
        public LevelPatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(IFormatMessage formatMessage)
        {
            Level level = formatMessage.Level;
            return level.Name.ToUpper();
        }
    }
}

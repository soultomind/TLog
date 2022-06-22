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

        public override string ConvertArgument(object obj = null)
        {
            Level logLevel = obj as Level;
            return logLevel.Name.ToUpper();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class MessagePatternLayout : PatternLayout
    {
        public MessagePatternLayout(string typeString) 
            : base(typeString)
        {

        }

        public override string ConvertArgument(IFormatMessage formatMessage)
        {
            return formatMessage.Message;
        }
    }
}

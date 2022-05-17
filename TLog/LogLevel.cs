using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class LogLevel
    {
        public static readonly LogLevel Trace = new LogLevel("Trace");
        public static readonly LogLevel Debug = new LogLevel("Debug");
        public static readonly LogLevel Info = new LogLevel("Info");
        public static readonly LogLevel Warn = new LogLevel("Warn");
        public static readonly LogLevel Error = new LogLevel("Error");
        public static readonly LogLevel Fatal = new LogLevel("Fatal");

        public string StrLevel { get; private set; }

        private LogLevel(string strLevel)
        {
            StrLevel = strLevel;
        }
    }
}

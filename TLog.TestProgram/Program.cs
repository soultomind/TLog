using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TLog
{
    class Program
    {
        static void Main(string[] args)
        {
            TLogger.DefaultPatternLayout = "%level %includefilter %date [Thread%thread-%C:%M] %message";
            TLogger.DebugViewIncludeFilter = "TLog";
            TLogger.Configure();

            TLogger.TraceWriteLine("TRACE");
            TLogger.DebugWriteLine("DEBUG");
            TLogger.InfoWriteLine("INFO");
            TLogger.WarnWriteLine("WARN");
            TLogger.ErrorWriteLine("ERROR");
            TLogger.FatalWriteLine("FATAL");

            System.Console.ReadKey();
        }
    }
}

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
            TLogger.DefaultPatternLayout = "%includefilter %level %date [%C:%M] %message%newline";
            TLogger.DebugViewIncludeFilter = "TLog";
            TLogger.Configure(Level.Warn);

            TLogger.Trace("TRACE");
            TLogger.Debug("DEBUG");
            TLogger.Info("INFO");
            TLogger.Warn("WARN");
            TLogger.Error("ERROR");
            TLogger.Fatal("FATAL");

            System.Console.ReadKey();
        }
    }
}

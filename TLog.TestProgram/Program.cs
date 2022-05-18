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
            TLogger.WriteLine("출력");

            TLogger.DefaultPatternLayout 
                = "%includefilter %level %date [%threadThread-%C:%M] %message%newline";
            TLogger.Configure();
            TLogger.DebugWrite("프로그램 시작");
            Console.ReadLine();

            for (int i = 0; i < 10; i++)
            {
                TLogger.DebugWrite(".");
                Thread.Sleep(500);
            }

            TLogger.DebugWrite("프로그램 종료");
        }
    }
}

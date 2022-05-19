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
            TLogger.DefaultPatternLayout 
                = "%includefilter %level %date [%threadThread-%C:%M] %message%newline";
            TLogger.Configure();

            Thread t1 = new Thread(Worker1);
            Thread t2 = new Thread(Worker2);
            Thread t3 = new Thread(Worker3);
            t1.Start();
            t2.Start();
            t3.Start();

            TLogger.DebugWrite("프로그램 시작");

            for (int i = 0; i < 10; i++)
            {
                TLogger.DebugWrite(".");
                Thread.Sleep(500);
            }

            t1.Join();
            t1 = null;

            TLogger.DebugWrite("프로그램 종료");
        }

        private static void Worker1()
        {
            TLogger.DebugWriteLine("Worker1");
        }

        private static void Worker2()
        {
            TLogger.DebugWriteLine("Worker2");
        }

        private static void Worker3()
        {
            TLogger.DebugWriteLine("Worker3");
        }
    }
}

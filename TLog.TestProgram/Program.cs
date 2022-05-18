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

            TLogger.Configure();
            TLogger.DebugWriteLine("프로그램 시작");
            Console.ReadLine();

            for (int i = 0; i < 10; i++)
            {
                TLogger.DebugWriteLine(".");
                Thread.Sleep(500);
            }

            TLogger.DebugWriteLine("프로그램 종료");
        }
    }
}

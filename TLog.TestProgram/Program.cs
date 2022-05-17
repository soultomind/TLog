using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    class Program
    {
        static void Main(string[] args)
        {
            TLogger.WriteLine("출력");

            string layout = TLogger.ConsolePatternLayout;
            int indexOf = TLogger.Class.IndexOf(layout);
            TLogger.WriteLine("Method.IndexOf=" + indexOf);
            indexOf = TLogger.Message.IndexOf(layout);
            TLogger.WriteLine("Method.IndexOf=" + indexOf);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceLog
{
    /// <summary>
    /// <see cref="System.Diagnostics.Trace"/> 로그 클래스입니다.
    /// </summary>
    public class TLogger
    {
        public static string PatternLayout = "%c:%m";
        private static string MakeHeader()
        {
            string className = new StackFrame(2).GetMethod().ReflectedType.Name;
            string methodName = new StackFrame(2, true).GetMethod().Name;
            return String.Format("{0}:{1}", className, methodName);
        }
        public static void Write(string message)
        {
            string header = MakeHeader();

            message = String.Format("{0} {1}", header, message);
            Trace.Write(message);
        }

        public static void WriteLine(string message)
        {
            string header = MakeHeader();

            message = String.Format("{0} {1}", header, message);
            Trace.WriteLine(message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    /// <summary>
    /// <see cref="System.Diagnostics.Trace"/> 로그 클래스입니다.
    /// </summary>
    public class TLogger
    {
        public static string PatternLayout = "%c:%m";

        private static string MethodReflectedTypeName
        {
            get { return new StackFrame(2).GetMethod().ReflectedType.Name; }
        }

        private static string MethodName
        {
            get { return new StackFrame(2).GetMethod().Name; }
        }

        private static string MakeHeader()
        {
            return String.Format("{0}:{1}", MethodReflectedTypeName, MethodName);
        }
        public static void Write(string message)
        {
            string header = MakeHeader();

            message = String.Format("{0}{1}", header, message);
            Trace.Write(message);
        }

        public static void WriteLine(string message)
        {
            string header = MakeHeader();

            message = String.Format("{0}{1}", header, message);
            Trace.WriteLine(message);
        }
    }
}

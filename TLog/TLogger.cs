using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    /// <summary>
    /// <see cref="System.Diagnostics.Trace"/> 로그 클래스입니다.
    /// </summary>
    public class TLogger
    {
        /// <summary>
        /// 클래스명
        /// </summary>
        public static readonly PatternLayoutTypeString Class = new PatternLayoutTypeString("C");

        /// <summary>
        /// 현재날짜
        /// </summary>
        public static readonly PatternLayoutTypeString Date = new PatternLayoutTypeString("date");

        /// <summary>
        /// DebugView Filter Include Filter 값
        /// </summary>
        public static readonly PatternLayoutTypeString IncludeFilter = new PatternLayoutTypeString("includefilter");

        /// <summary>
        /// 로그레벨
        /// </summary>
        public static readonly PatternLayoutTypeString Level = new PatternLayoutTypeString("level");

        /// <summary>
        /// 메소드명
        /// </summary>
        public static readonly PatternLayoutTypeString Method = new PatternLayoutTypeString("M");

        /// <summary>
        /// 메시지
        /// </summary>
        public static readonly PatternLayoutTypeString Message = new PatternLayoutTypeString("message");

        /// <summary>
        /// 개행
        /// </summary>
        public static readonly PatternLayoutTypeString NewLine = new PatternLayoutTypeString("newline");

        /// <summary>
        /// 현재스레드
        /// </summary>
        public static readonly PatternLayoutTypeString Thread = new PatternLayoutTypeString("thread");

        private static readonly PatternLayoutTypeString[] LayoutTypeStrings = new PatternLayoutTypeString[] 
        {
            Class, Date, IncludeFilter, Level, Method, Message, NewLine, Thread

        };
        /// <summary>
        /// <see cref="System.Diagnostics.ConsoleTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static string ConsolePatternLayout = "%level %a %c:%m %date %message";

        /// <summary>
        /// <see cref="System.Diagnostics.TextWriterTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        public static string TextWriterLayout = "%level %a %C:%M %date %message";

        /// <summary>
        /// <see cref="System.Diagnostics.EventLogTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        public static string EventLogLayout = "%level %a %C:%M %date %message";

        /// <summary>
        /// %date 패턴 사용시 사용되는 포맷입니다.
        /// </summary>
        public static string DateTimeFormat = "yyyy-MM-dd hh:mm:ss:ffff";

        private static readonly int SkipFrames = 2;

        private static bool HasStackFrameLayout(Type listenerType)
        {
            foreach (TraceListener itemListener in Trace.Listeners)
            {
                if (itemListener.GetType() == listenerType.GetType())
                {
                    return true;
                }
            }
            return false;
        }

        private static StackFrame NewStackFrame(int skipFrames = 2, bool fNeedFileInfo = true)
        {
            return new StackFrame(skipFrames, fNeedFileInfo);
        }

        private static string MakePatternLayout()
        {
            StackFrame sf = NewStackFrame();
            return String.Format("{0}:{1}", sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name);
        }

        private static string DateTimeNowString()
        {
            return DateTime.Now.ToString(DateTimeFormat);
        }

        #region ConsoleTraceListener

        private static string MakeConsoleTraceListenerMessage(LogLevel level, string message)
        {
            List<string> format = new List<string>();
            StringBuilder builder = new StringBuilder();

            StackFrame sf = null;
            // TOOD: 아래 패턴레이아웃에서 사용되는 모든 타입에 대해서 위치 값 구한후 Format 설정필요.
            if (Class.IndexOf(ConsolePatternLayout) != -1 || Method.IndexOf(ConsolePatternLayout) != -1)
            {
                sf = NewStackFrame(3, true);
            }

            if (Class.IndexOf(ConsolePatternLayout) != -1)
            {
                format.Add(sf.GetMethod().ReflectedType.Name);
            }



            if (ConsolePatternLayout.IndexOf("%date") != -1)
            {
                format.Add(DateTimeNowString());
            }

            if (ConsolePatternLayout.IndexOf("%level") != -1)
            {
                format.Add(level.StrLevel);
            }

            if (ConsolePatternLayout.IndexOf("%m") != -1)
            {
                format.Add(sf.GetMethod().Name);
            }

            if (ConsolePatternLayout.IndexOf("%message") != -1)
            {
                format.Add(message);
            }

            if (ConsolePatternLayout.IndexOf("%thread") != -1)
            {
                format.Add(System.Threading.Thread.CurrentThread.Name);
            }

            return builder.ToString();
        }

        #endregion

        public static void Configure()
        {
            if (HasStackFrameLayout(typeof(ConsoleTraceListener)))
            {

            }

            if (HasStackFrameLayout(typeof(TextWriterTraceListener)))
            {

            }

            if (HasStackFrameLayout(typeof(EventLogTraceListener)))
            {
                
            }
        }

        public static void WriteLine(string message)
        {
            if (HasStackFrameLayout(typeof(ConsoleTraceListener)))
            {
                // TODO: ConsoleTraceListener 출력

            }

            if (HasStackFrameLayout(typeof(TextWriterTraceListener)))
            {
                // TODO: TextWriterTraceListener 출력

            }

            if (HasStackFrameLayout(typeof(EventLogTraceListener)))
            {
                // TODO: EventLogTraceListener 출력
            }

            string header = MakePatternLayout();

            message = String.Format("{0} {1} {2}", header, DateTimeNowString(), message);
            Trace.WriteLine(message);

            
        }
    }
}

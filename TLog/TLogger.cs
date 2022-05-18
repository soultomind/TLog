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
        public static readonly IPatternLayoutType Class = new ClassPatternLayout("C");

        /// <summary>
        /// 현재날짜
        /// </summary>
        public static readonly IPatternLayoutType Date = new DatePatternLayout("date");

        /// <summary>
        /// DebugView Filter Include Filter 값
        /// </summary>
        public static readonly IPatternLayoutType IncludeFilter = new IncludeFilterPatternLayout("includefilter");

        /// <summary>
        /// 로그레벨
        /// </summary>
        public static readonly IPatternLayoutType Level = new LevelPatternLayout("level");

        /// <summary>
        /// 메소드명
        /// </summary>
        public static readonly IPatternLayoutType Method = new MethodPatternLayout("M");

        /// <summary>
        /// 메시지
        /// </summary>
        public static readonly IPatternLayoutType Message = new MessagePatternLayout("message");

        /// <summary>
        /// 개행
        /// </summary>
        public static readonly IPatternLayoutType NewLine = new NewLinePatternLayout("newline");

        /// <summary>
        /// 현재스레드
        /// </summary>
        public static readonly IPatternLayoutType Thread = new ThreadPatternLayout("thread");

        private static readonly IPatternLayoutType[] LayoutTypeStrings = new IPatternLayoutType[] 
        {
            Class, Date, IncludeFilter, Level, Method, Message, NewLine, Thread

        };
        /// <summary>
        /// <see cref="System.Diagnostics.ConsoleTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static string DefaultPatternLayout = "%level %includefilter %C:%M %date %message";
        private static List<PatternLayoutTypeComparable> DefaultPatternLayoutTypes;
        private static string DefaultPatternFormat = String.Empty;


        /// <summary>
        /// <see cref="System.Diagnostics.TextWriterTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        public static string TextWriterLayout = DefaultPatternLayout;

        /// <summary>
        /// <see cref="System.Diagnostics.EventLogTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        public static string EventLogLayout = DefaultPatternLayout;

        /// <summary>
        /// %date 패턴 사용시 사용되는 포맷입니다.
        /// </summary>
        public static string DateTimeFormat = "yyyy-MM-dd hh:mm:ss:ffff";

        #region Private
        private static bool HasStackFrameLayout(string typeName)
        {
            // TODO: typeof(DefaultTraceListener) 시에 Type.Name 값이 RuntimeType 조사필요
            foreach (TraceListener itemListener in Trace.Listeners)
            {
                Type type = itemListener.GetType();
                if (type.Name == typeName)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsReflectPatternLayout(IPatternLayoutType layoutType)
        {
            return layoutType.GetType() == Class.GetType() || layoutType.GetType() == Method.GetType();
        }

        private static bool HasReflectPatternLayout(List<PatternLayoutTypeComparable> layoutTypeComparables)
        {
            foreach (var layoutTypeComparable in layoutTypeComparables)
            {
                IPatternLayoutType layoutType = layoutTypeComparable.LayoutType;
                if (IsReflectPatternLayout(layoutType))
                {
                    return true;
                }
            }
            return false;
        }

        private static string CreateFormatText(string[] args)
        {
            if (DefaultPatternFormat == String.Empty)
            {
                // "%level %includefilter %C:%M %date %message";
                string format = DefaultPatternLayout;
                for (int i = 0; i < DefaultPatternLayoutTypes.Count; i++)
                {
                    IPatternLayoutType layoutType = DefaultPatternLayoutTypes[i].LayoutType;
                    format = format.Replace(layoutType.LayoutTypeString, "{" + i + "}");
                }

                DefaultPatternFormat = format;
                return String.Format(format, args);
            }

            return String.Format(DefaultPatternFormat, args);
        }
        #endregion

        /// <summary>
        /// 디버그뷰에서 사용되는 Filter/Include Filter 에 사용되는 속성입니다.
        /// </summary>
        public static string DebugViewIncludeFilter
        {
            get { return (IncludeFilter as IncludeFilterPatternLayout).IncludeFilter; }
            set { (IncludeFilter as IncludeFilterPatternLayout).IncludeFilter = value; }
        }

        public static void Configure()
        {
            List<string> format = new List<string>();
            StringBuilder builder = new StringBuilder();

            int indexOf = -1;

            // TOOD: 아래 패턴레이아웃에서 사용되는 모든 타입에 대해서 위치 값 구한후 Format 설정필요.
            if (HasStackFrameLayout("DefaultTraceListener"))
            {
                List<PatternLayoutTypeComparable> list = new List<PatternLayoutTypeComparable>();
                foreach (PatternLayoutType layoutType in LayoutTypeStrings)
                {
                    indexOf = layoutType.IndexOf(DefaultPatternLayout);
                    if (indexOf != -1)
                    {
                        list.Add(new PatternLayoutTypeComparable(indexOf, layoutType));
                    }
                }

                list.Sort();
                DefaultPatternLayoutTypes = list;
                
                // 다시 루프 돌면서 
            }

            #region TODO TextWriter, EventLog 작업
            
            //TextWriterTraceListener
            //EventLogTraceListener 
            
            #endregion
        }

        private static string MakeMessage(LogLevel logLevel, string message)
        {
            // TOOD: Debug 로그레벨이 현재 사용가능한지 체크 후 출력 처리

            // "%level %a %C:%M %date %message";

            string[] args = new string[DefaultPatternLayoutTypes.Count];
            StackFrame sf = null;
            if (HasReflectPatternLayout(DefaultPatternLayoutTypes))
            {
                sf = new StackFrame(2, true);
            }

            string arg = String.Empty;
            for (int i = 0; i < DefaultPatternLayoutTypes.Count; i++)
            {
                // Class, Date, IncludeFilter, Level, Method, Message, NewLine, Thread
                IPatternLayoutType layoutType = DefaultPatternLayoutTypes[i].LayoutType;
                if (IsReflectPatternLayout(layoutType))
                {
                    arg = layoutType.ConvertArgument(sf);
                }
                else if (layoutType.GetType() == Date.GetType())
                {
                    arg = layoutType.ConvertArgument(DateTimeFormat);
                }
                else if (layoutType.GetType() == Level.GetType())
                {
                    arg = layoutType.ConvertArgument(LogLevel.Debug);
                }
                else if (layoutType.GetType() == Message.GetType())
                {
                    arg = layoutType.ConvertArgument(message);
                }
                else
                {
                    arg = layoutType.ConvertArgument();
                }
                args[i] = arg;
            }

            message = CreateFormatText(args);
            return message;
        }

        public static void DebugWrite(string message)
        {
            message = MakeMessage(LogLevel.Debug, message);
            Trace.Write(message);
        }

        public static void DebugWriteLine(string message)
        {
            message = MakeMessage(LogLevel.Debug, message);
            Trace.WriteLine(message);
        }

        public static void Write(string message)
        {
            StackFrame sf = new StackFrame(1, true);
            string header = String.Format("{0}:{1}", sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name);

            message = String.Format("{0} {1} {2}", header, DateTime.Now.ToString(DateTimeFormat), message);
            Trace.Write(message);
        }

        public static void WriteLine(string message)
        {
            StackFrame sf = new StackFrame(1, true);
            string header = String.Format("{0}:{1}", sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name);

            message = String.Format("{0} {1} {2}", header, DateTime.Now.ToString(DateTimeFormat), message);
            Trace.WriteLine(message);
        }
    }
}

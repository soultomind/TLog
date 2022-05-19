﻿using System;
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
        public static bool IsConfigure { get; private set; }

        /// <summary>
        /// 클래스명 패턴레이아웃 (%C)
        /// </summary>
        public static readonly IPatternLayoutType Class = new ClassPatternLayout("C");

        /// <summary>
        /// 현재날짜 패턴레이아웃 (%date)
        /// </summary>
        public static readonly IPatternLayoutType Date = new DatePatternLayout("date")
            { Format = "yyyy-MM-dd hh:mm:ss:ffff" };

        /// <summary>
        /// DebugView Filter Include 값 패턴레이아웃 (%includefilter)
        /// </summary>
        public static readonly IPatternLayoutType IncludeFilter = new IncludeFilterPatternLayout("includefilter")
            { IncludeFilter = Assembly.GetAssembly(typeof(IncludeFilterPatternLayout)).GetName().Name };

        /// <summary>
        /// 로그레벨 패턴레이아웃 (%level)
        /// </summary>
        public static readonly IPatternLayoutType Level = new LevelPatternLayout("level");

        /// <summary>
        /// 메소드명 패턴레이아웃 (%M)
        /// </summary>
        public static readonly IPatternLayoutType Method = new MethodPatternLayout("M");

        /// <summary>
        /// 메시지 패턴레이아웃 (%message)
        /// </summary>
        public static readonly IPatternLayoutType Message = new MessagePatternLayout("message");

        /// <summary>
        /// 개행 패턴레이아웃 (%newline)
        /// </summary>
        public static readonly IPatternLayoutType NewLine = new NewLinePatternLayout("newline");

        /// <summary>
        /// 현재스레드 패턴레이아웃 (%thread)
        /// </summary>
        public static readonly IPatternLayoutType Thread = new ThreadPatternLayout("thread");

        /// <summary>
        /// 패턴레이아웃 배열
        /// </summary>
        public static readonly IPatternLayoutType[] LayoutTypes = new IPatternLayoutType[] 
        {
            Class, Date, IncludeFilter, Level, Method, Message, NewLine, Thread
        };

        /// <summary>
        /// <see cref="System.Diagnostics.DefaultTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static string DefaultPatternLayout
        {
            get { return _DefaultPatternLayout; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }

                if (value.IndexOf("%message") == -1)
                {
                    throw new ArgumentException("The value must contain a %message");
                }
                _DefaultPatternLayout = value;
            }
        }
        private static string _DefaultPatternLayout = "%includefilter %date %level %message%newline"; // "%level %includefilter %C:%M %date %message";
        private static PatternLayoutFormat LayoutFormat = new PatternLayoutFormat();


        /// <summary>
        /// <see cref="System.Diagnostics.TextWriterTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        public static string TextWriterLayout = DefaultPatternLayout;

        /// <summary>
        /// <see cref="System.Diagnostics.EventLogTraceListener"/>에 출력하는 패턴 레이아웃입니다.
        /// </summary>
        public static string EventLogLayout = DefaultPatternLayout;

        #region Private
        private static bool HasStackFrameLayout(string typeName)
        {
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

        private static string MakeMessage(LogLevel logLevel, string message)
        {
            // TOOD: 로그레벨이 현재 사용가능한지 체크 후 출력 처리

            string[] args = new string[LayoutFormat.TypeOrders.Count];
            StackFrame sf = null;
            if (HasReflectPatternLayout(LayoutFormat.TypeOrders))
            {
                sf = new StackFrame(2, true);
            }

            string arg = String.Empty;
            for (int i = 0; i < LayoutFormat.TypeOrders.Count; i++)
            {
                // Class, Date, IncludeFilter, Level, Method, Message, NewLine, Thread
                IPatternLayoutType layoutType = LayoutFormat.TypeOrders[i].LayoutType;
                if (IsReflectPatternLayout(layoutType))
                {
                    arg = layoutType.ConvertArgument(sf);
                }
                else if (layoutType.GetType() == Level.GetType())
                {
                    arg = layoutType.ConvertArgument(logLevel);
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

            message = LayoutFormat.CreateFormatMessage(DefaultPatternLayout, args);
            return message;
        }

        #endregion

        /// <summary>
        /// %date 패턴 사용시 사용되는 포맷입니다.
        /// </summary>
        public static string DateTimeFormat
        {
            get { return (Date as DatePatternLayout).Format; }
            set { (Date as DatePatternLayout).Format = value; }
        }

        /// <summary>
        /// 디버그뷰에서 사용되는 Filter/Include 값에 적용할 수 있는 속성입니다.
        /// </summary>
        public static string DebugViewIncludeFilter
        {
            get { return (IncludeFilter as IncludeFilterPatternLayout).IncludeFilter; }
            set { (IncludeFilter as IncludeFilterPatternLayout).IncludeFilter = value; }
        }


        /// <summary>
        /// 속성에 대한 설정을 적용합니다.
        /// </summary>
        public static void Configure()
        {
            if (!IsConfigure)
            {
                WriteLine("PatternLayout=" + DefaultPatternLayout);
                // TODO: typeof(DefaultTraceListener) 시에 Type.Name 값이 RuntimeType 조사필요
                if (HasStackFrameLayout("DefaultTraceListener"))
                {
                    LayoutFormat.Process(LayoutTypes, DefaultPatternLayout);
                    WriteLine("LayoutFormat.Process");
                }

                #region TODO TextWriter, EventLog 작업

                //TextWriterTraceListener
                //EventLogTraceListener 

                #endregion

                IsConfigure = true;
            }
        }

        internal static void WriteLine(string message)
        {
            Trace.WriteLine("TLog :: " + message);
        }

        public static void TraceWrite(string message)
        {
            message = MakeMessage(LogLevel.Trace, message);
            Trace.Write(message);
        }

        public static void TraceWriteLine(string message)
        {
            message = MakeMessage(LogLevel.Trace, message);
            Trace.WriteLine(message);
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

        public static void InfoWrite(string message)
        {
            message = MakeMessage(LogLevel.Info, message);
            Trace.Write(message);
        }

        public static void InfoWriteLine(string message)
        {
            message = MakeMessage(LogLevel.Info, message);
            Trace.WriteLine(message);
        }

        public static void WarnWrite(string message)
        {
            message = MakeMessage(LogLevel.Warn, message);
            Trace.Write(message);
        }

        public static void WarnWriteLine(string message)
        {
            message = MakeMessage(LogLevel.Warn, message);
            Trace.WriteLine(message);
        }

        public static void ErrorWrite(string message)
        {
            message = MakeMessage(LogLevel.Error, message);
            Trace.Write(message);
        }

        public static void ErrorWriteLine(string message)
        {
            message = MakeMessage(LogLevel.Error, message);
            Trace.WriteLine(message);
        }

        public static void FatalWrite(string message)
        {
            message = MakeMessage(LogLevel.Fatal, message);
            Trace.Write(message);
        }

        public static void FatalWriteLine(string message)
        {
            message = MakeMessage(LogLevel.Fatal, message);
            Trace.WriteLine(message);
        }
    }
}

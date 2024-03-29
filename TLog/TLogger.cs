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
        private static readonly object _ConfigureLock = new object();
        public static bool IsConfigure
        {
            get { return _IsConfigure; }
            private set { _IsConfigure = value; }
        }
        private static volatile bool _IsConfigure;

        /// <summary>
        /// 클래스명 패턴레이아웃 (%C)
        /// </summary>
        public static readonly IPatternLayout Class = new ClassPatternLayout("C");

        /// <summary>
        /// 현재날짜 패턴레이아웃 (%date)
        /// </summary>
        public static readonly IPatternLayout Date = new DatePatternLayout("date")
            { Format = "yyyy-MM-dd hh:mm:ss:ffff" };

        /// <summary>
        /// DebugTool Filter Include 값 패턴레이아웃 (%includefilter)
        /// </summary>
        public static readonly IPatternLayout IncludeFilter = new IncludeFilterPatternLayout("includefilter")
            { IncludeFilter = Assembly.GetAssembly(typeof(IncludeFilterPatternLayout)).GetName().Name };

        /// <summary>
        /// 로그레벨 패턴레이아웃 (%level)
        /// </summary>
        public static readonly IPatternLayout Level = new LevelPatternLayout("level");

        /// <summary>
        /// 메소드명 패턴레이아웃 (%M)
        /// </summary>
        public static readonly IPatternLayout Method = new MethodPatternLayout("M");

        /// <summary>
        /// 메시지 패턴레이아웃 (%message)
        /// </summary>
        public static readonly IPatternLayout Message = new MessagePatternLayout("message");

        /// <summary>
        /// 개행 패턴레이아웃 (%newline)
        /// </summary>
        public static readonly IPatternLayout NewLine = new NewLinePatternLayout("newline");

        /// <summary>
        /// 현재스레드 패턴레이아웃 (%thread)
        /// </summary>
        public static readonly IPatternLayout Thread = new ThreadPatternLayout("thread");

        /// <summary>
        /// 패턴레이아웃 배열
        /// </summary>
        public static readonly IPatternLayout[] Layouts = new IPatternLayout[] 
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

                if (value.IndexOf("%newline") == -1)
                {
                    throw new ArgumentException("The value must contain a %newline");
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

        private static Level _UseLevel;
        

        #region Private
        private static bool HasStackFrameLayout(string typeName)
        {
            foreach (TraceListener itemListener in System.Diagnostics.Trace.Listeners)
            {
                Type type = itemListener.GetType();
                if (type.Name == typeName)
                {
                    return true;
                }
            }
            return false;
        }

        private static string CreateLayoutMessage(Level logLevel, string message)
        {
            IFormatMessage formatMessage = new FormatMessage();
            formatMessage.Level = logLevel;
            formatMessage.Message = message;
            if (HasReflectPatternLayout(LayoutFormat.TypeOrders))
            {
                formatMessage.StackFrame = new StackFrame(2, true);
            }

            string[] args = CreateLayoutTypeArguments(formatMessage);
            message = LayoutFormat.CreateFormatMessage(DefaultPatternLayout, args);
            return message;
        }

        private static bool HasReflectPatternLayout(List<PatternLayoutTypeComparable> layoutTypeComparables)
        {
            foreach (var layoutTypeComparable in layoutTypeComparables)
            {
                IPatternLayout layoutType = layoutTypeComparable.LayoutType;
                if (IsReflectPatternLayout(layoutType))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsReflectPatternLayout(IPatternLayout layoutType)
        {
            return layoutType.GetType() == Class.GetType() || layoutType.GetType() == Method.GetType();
        }

        private static string[] CreateLayoutTypeArguments(IFormatMessage formatMessage)
        {
            string[] args = new string[LayoutFormat.TypeOrders.Count];
            for (int i = 0; i < LayoutFormat.TypeOrders.Count; i++)
            {
                // Class, Date, IncludeFilter, Level, Method, Message, NewLine, Thread
                IPatternLayout layoutType = LayoutFormat.TypeOrders[i].LayoutType;
                args[i] = layoutType.ConvertArgument(formatMessage);
            }
            return args;
        }

        #endregion

        public static bool IsFullName
        {
            get { return (Class as ClassPatternLayout).IsFullName; }
            set
            {
                if (!IsConfigure)
                {
                    (Class as ClassPatternLayout).IsFullName = value;
                }
            }
        }

        /// <summary>
        /// %date 패턴 사용시 사용되는 포맷입니다.
        /// </summary>
        public static string DateTimeFormat
        {
            get { return (Date as DatePatternLayout).Format; }
            set
            {
                if (!IsConfigure)
                {
                    (Date as DatePatternLayout).Format = value;
                }
            }
        }

        /// <summary>
        /// 디버그툴에서 사용되는 Filter/Include 값에 적용할 수 있는 속성입니다.
        /// </summary>
        public static string DebugToolIncludeFilter
        {
            get { return (IncludeFilter as IncludeFilterPatternLayout).IncludeFilter; }
            set
            {
                if (!IsConfigure)
                {
                    (IncludeFilter as IncludeFilterPatternLayout).IncludeFilter = value;
                }
            }
        }

        public static bool IsEnabled(Level level)
        {
            return _UseLevel.IsEnabled(level);
        }

        public static void Configure()
        {
            Configure(TLog.Level.Debug);
        }

        /// <summary>
        /// 속성에 대한 설정을 적용합니다.
        /// </summary>
        public static void Configure(Level level)
        {
            if (!IsConfigure)
            {
                lock (_ConfigureLock)
                {
                    if (!IsConfigure)
                    {
                        _UseLevel = level;
                        WriteLine("PatternLayout=" + DefaultPatternLayout);
                        // TODO: typeof(DefaultTraceListener) 시에 Type.Name 값이 RuntimeType 조사필요
                        if (HasStackFrameLayout("DefaultTraceListener"))
                        {
                            LayoutFormat.Process(Layouts, DefaultPatternLayout);
                            WriteLine("LayoutFormat.Process");
                        }

                        #region TODO TextWriter, EventLog 작업

                        // TextWriterTraceListener
                        // EventLogTraceListener 

                        #endregion

                        IsConfigure = true;
                    }
                }
            }
        }

        internal static void WriteLine(string message)
        {
            System.Diagnostics.Trace.WriteLine("TLog :: " + message);
        }

        public static void Trace(string message)
        {
            if (IsConfigure && IsEnabled(TLog.Level.Trace))
            {
                message = CreateLayoutMessage(TLog.Level.Trace, message);
                System.Diagnostics.Trace.Write(message); 
            }
        }

        public static void Debug(string message)
        {
            if (IsConfigure && IsEnabled(TLog.Level.Debug))
            {
                message = CreateLayoutMessage(TLog.Level.Debug, message);
                System.Diagnostics.Trace.Write(message); 
            }
        }

        public static void Info(string message)
        {
            if (IsConfigure && IsEnabled(TLog.Level.Info))
            {
                message = CreateLayoutMessage(TLog.Level.Info, message);
                System.Diagnostics.Trace.Write(message); 
            }
        }

        public static void Warn(string message)
        {
            if (IsConfigure && IsEnabled(TLog.Level.Warn))
            {
                message = CreateLayoutMessage(TLog.Level.Warn, message);
                System.Diagnostics.Trace.Write(message); 
            }
        }

        public static void Error(string message)
        {
            if (IsConfigure && IsEnabled(TLog.Level.Error))
            {
                message = CreateLayoutMessage(TLog.Level.Error, message);
                System.Diagnostics.Trace.Write(message); 
            }
        }

        public static void Fatal(string message)
        {
            if (IsConfigure && IsEnabled(TLog.Level.Fatal))
            {
                message = CreateLayoutMessage(TLog.Level.Fatal, message);
                System.Diagnostics.Trace.Write(message);
            }
        }
    }
}

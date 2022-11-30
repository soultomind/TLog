using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class FormatMessage : IFormatMessage
    {
        public StringBuilder Builder { get; set; }
        public StackFrame StackFrame { get; set; }
        public Level Level { get; set; }
        public string Message { get; set; }

        public FormatMessage(int capacity = 128)
        {
            Builder = new StringBuilder(capacity);
        }
    }
}

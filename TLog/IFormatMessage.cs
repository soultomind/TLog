using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public interface IFormatMessage
    {
        StringBuilder Builder { get; set; }
        StackFrame StackFrame { get; set; }
        Level Level { get; set; }
        string Message { get; set; }
    }
}

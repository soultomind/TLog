using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public interface IPatternLayout
    {
        string TypeString { get; }

        string LayoutTypeString { get; }

        int IndexOf(string input);

        string ConvertArgument(object obj = null);
    }
}

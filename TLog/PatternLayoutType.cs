using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TLog
{
    public abstract class PatternLayoutType : IPatternLayoutType
    {
        public string TypeString { get; private set; }

        public PatternLayoutType(string typeString)
        {
            TypeString = typeString;
        }

        public string LayoutTypeString
        {
            get { return "%" + TypeString; }
        }

        public int IndexOf(string input)
        {
            Regex regex = new Regex(LayoutTypeString);
            if (regex.IsMatch(input))
            {
                Match match = regex.Match(input);
                return match.Index;
            }
            return -1;
        }

        public abstract string ConvertArgument(object obj = null);
    }
}

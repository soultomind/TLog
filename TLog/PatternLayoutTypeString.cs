using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TLog
{
    public class PatternLayoutTypeString
    {
        public string LayoutType { get; internal set; }

        public PatternLayoutTypeString(string layoutType)
        {
            LayoutType = layoutType;
        }

        public string PatternLayoutType
        {
            get { return "%" + LayoutType; }
        }

        public int IndexOf(string input)
        {
            Regex regex = new Regex(PatternLayoutType);
            if (regex.IsMatch(input))
            {
                Match match = regex.Match(input);
                return match.Index;
            }
            return -1;
        }
    }
}

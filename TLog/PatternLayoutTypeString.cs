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
        public string CharString { get; internal set; }

        public PatternLayoutTypeString(string charString)
        {
            CharString = charString;
        }

        public string LayoutCharString
        {
            get { return "%" + CharString; }
        }

        public int IndexOf(string input)
        {
            Regex regex = new Regex(LayoutCharString);
            if (regex.IsMatch(input))
            {
                Match match = regex.Match(input);
                return match.Index;
            }
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class PatternLayoutFormat
    {
        public string UseCurrentPatternFormat { get; set; } = String.Empty;
        public List<PatternLayoutTypeComparable> TypeOrders
        {
            get;
            private set;
        }

        private static readonly object _Lock = new object();
        public PatternLayoutFormat()
        {
            TypeOrders = new List<PatternLayoutTypeComparable>();
        }

        public void Process(IPatternLayoutType[] layoutTypes, string patternLayoutFormat)
        {
            List<PatternLayoutTypeComparable> list = new List<PatternLayoutTypeComparable>();

            int indexOf = -1;
            foreach (PatternLayoutType layoutType in layoutTypes)
            {
                indexOf = layoutType.IndexOf(patternLayoutFormat);
                if (indexOf != -1)
                {
                    list.Add(new PatternLayoutTypeComparable(indexOf, layoutType));
                }
            }

            list.Sort();
            TypeOrders = list;
        }

        public string CreateFormatMessage(string patternLayoutFormat, string[] args)
        {
            if (UseCurrentPatternFormat == String.Empty)
            {
                lock (_Lock)
                {
                    if (UseCurrentPatternFormat == String.Empty)
                    {
                        Trace.WriteLine("Set UseCurrentPatternFormat!");
                        string format = patternLayoutFormat;
                        for (int i = 0; i < TypeOrders.Count; i++)
                        {
                            IPatternLayoutType layoutType = TypeOrders[i].LayoutType;
                            format = format.Replace(layoutType.LayoutTypeString, "{" + i + "}");
                        }

                        UseCurrentPatternFormat = format;
                    }
                }                
            }
            return String.Format(UseCurrentPatternFormat, args);
        }
    }
}

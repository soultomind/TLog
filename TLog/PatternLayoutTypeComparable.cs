using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class PatternLayoutTypeComparable : IComparable
    {
        public int IndexOf { get; private set; }
        public IPatternLayout LayoutType { get; private set; }

        public PatternLayoutTypeComparable(int indexOf, IPatternLayout layoutType)
        {
            IndexOf = indexOf;
            LayoutType = layoutType;
        }

        public int CompareTo(object obj)
        {
            PatternLayoutTypeComparable thisObj = this;
            PatternLayoutTypeComparable otherObj = obj as PatternLayoutTypeComparable;
            return thisObj.IndexOf.CompareTo(IndexOf);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLog
{
    public class Level
    {
        public static readonly Level Trace = new Level("Trace", 10000);
        public static readonly Level Debug = new Level("Debug", 9000);
        public static readonly Level Info = new Level("Info", 8000);
        public static readonly Level Warn = new Level("Warn", 7000);
        public static readonly Level Error = new Level("Error", 6000);
        public static readonly Level Fatal = new Level("Fatal", 5000);

        public string Name { get; private set; }
        public int IntValue { get; private set; }

        private Level(string name, int intValue)
        {
            Name = name;
            IntValue = intValue;
        }

        public bool IsEnabled(Level level)
        {
            if (level.IntValue <= IntValue)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name.ToUpper();
        }
    }
}

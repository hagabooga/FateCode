using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class MaxStat : BaseStat
    {
        public event Handlers.CurrentMax currentMaxChange;

        private int _current;

        public int Current
        {
            get => _current;
            set
            {
                _current = Math.Max(_current + value, Value);
            }
        }

        public MaxStat(int @base) : base(@base)
        {
            Current = Base;
        }

        public static implicit operator int(MaxStat currentStat) => currentStat.Current;
        public static implicit operator MaxStat(int value) => new MaxStat(value);

        public void Full() => Current = Value;
    }
}
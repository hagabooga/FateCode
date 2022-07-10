using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class CurrentStat : BaseStat
    {
        private int _current;

        public int Current
        {
            get => _current;
            set
            {
                _current = Math.Max(_current + value, Value);
            }
        }

        public CurrentStat(int @base) : base(@base)
        {
            Current = Base;
        }

        public static implicit operator int(CurrentStat currentStat) => currentStat.Current;
        public static implicit operator CurrentStat(int value) => new CurrentStat(value);
    }
}
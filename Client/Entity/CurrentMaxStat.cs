using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class CurrentMaxStat : BaseStat
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

        public CurrentMaxStat(int @base) : base(@base)
        {
            Current = Base;
        }

        public static implicit operator int(CurrentMaxStat currentStat) => currentStat.Current;
        public static implicit operator CurrentMaxStat(int value) => new CurrentMaxStat(value);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class BaseStat
    {
        public event Action baseChanged;

        readonly Dictionary<int, int> bonuses = new Dictionary<int, int>();

        public int Base { get; }
        public int Value => Base + bonuses.Sum(x => x.Key * x.Value);

        public BaseStat(int @base)
        {
            Base = @base;
        }

        public static implicit operator int(BaseStat baseStat) => baseStat.Value;
        public static implicit operator BaseStat(int value) => new BaseStat(value);

        public void Buff(int value)
        {
            if (!bonuses.ContainsKey(value))
            {
                bonuses[value] = 1;
            }

            else
            {
                bonuses[value]++;
            }
        }

        public void RemoveBuff(int value)
        {
            if (!bonuses.ContainsKey(value))
            {
                throw new Exception($"Stat does not contain buff: {value}");
            }
            if (--bonuses[Value] == 0)
            {
                bonuses.Remove(value);
            }
        }
    }
}
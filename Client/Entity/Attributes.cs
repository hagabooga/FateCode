using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class Attributes
    {
        public BaseStat Strength { get; }
        public BaseStat Intelligence { get; }
        public BaseStat Agility { get; }
        public BaseStat Luck { get; }
        public BaseStat Physical { get; }
        public BaseStat Magical { get; }
        public BaseStat Armor { get; }
        public BaseStat Resist { get; }
        public BaseStat Hit { get; }
        public BaseStat Dodge { get; }
        public BaseStat Crit { get; }
        public BaseStat CritMulti { get; }
        public BaseStat AttackSpeed { get; }

        public CurrentMaxStat Health { get; }
        public CurrentMaxStat Mana { get; }
        public CurrentMaxStat Energy { get; }
        public CurrentMaxStat Experience { get; }
        public CurrentMaxStat AbilityPoints { get; }

        public int Level { get; private set; }
        public string Job { get; private set; }
    }
}
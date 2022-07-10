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

        public CurrentStat Health { get; }
        public CurrentStat Mana { get; }
        public CurrentStat Energy { get; }
        public CurrentStat Experience { get; }
        public CurrentStat AbilityPoints { get; private set; }

        public int Level { get; private set; }
        public string Job { get; private set; }

    }
}
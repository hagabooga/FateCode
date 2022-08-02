using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Client
{
    public class Attributes : EzPrefab
    {
        public BaseStat Strength { get; protected set; } = new BaseStat(1);
        public BaseStat Intelligence { get; protected set; } = new BaseStat(1);
        public BaseStat Agility { get; protected set; } = new BaseStat(1);
        public BaseStat Luck { get; protected set; } = new BaseStat(1);
        public BaseStat Physical { get; protected set; } = new BaseStat(1);
        public BaseStat Magical { get; protected set; } = new BaseStat(1);
        public BaseStat Armor { get; protected set; } = new BaseStat(1);
        public BaseStat Resist { get; protected set; } = new BaseStat(1);
        public BaseStat Hit { get; protected set; } = new BaseStat(1);
        public BaseStat Dodge { get; protected set; } = new BaseStat(1);
        public BaseStat Crit { get; protected set; } = new BaseStat(1);
        public BaseStat CritMulti { get; protected set; } = new BaseStat(1);
        public BaseStat AttackSpeed { get; protected set; } = new BaseStat(1);

        public MaxStat MaxHealth { get; protected set; } = new MaxStat(1);
        public MaxStat MaxMana { get; protected set; } = new MaxStat(1);
        public MaxStat MaxEnergy { get; protected set; } = new MaxStat(1);
        public MaxStat MaxExperience { get; protected set; } = new MaxStat(1);
        public MaxStat MaxAbilityPoints { get; protected set; } = new MaxStat(1);
        public MaxStat MaxSkillPoints { get; protected set; } = new MaxStat(1);

        public int Level { get; protected set; }
        public string Job { get; protected set; }

        public Attributes()
        {
            new[]
            {
                Strength,
                Intelligence,
                Agility,
                Luck,
                Physical,
                Magical,
                Armor,
                Resist,
                Hit,
                Dodge,
                Crit,
                CritMulti,
                AttackSpeed,
                MaxHealth,
                MaxMana,
                MaxEnergy,
                MaxExperience,
                MaxAbilityPoints,
                MaxSkillPoints,
            }
            .ForEach(x =>
            {
                x.baseChanged += UpdateStats;
            });
        }

        public void LevelUp()
        {
            MaxExperience.Current = 0;
            Level += 1;
            MaxAbilityPoints.Current += 3;
            MaxSkillPoints += 3;
            UpdateStats();
        }

        protected virtual void UpdateStats()
        {
        }

    }
}
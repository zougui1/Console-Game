using ConsoleGame.entity.stats;
using ConsoleGame.json;
using ConsoleGame.utils;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConsoleGame.entity.managers
{
    public class LevelingManager
    {
        private StatUnits StrengthMage { get; set; }
        private StatUnits DefAgi { get; set; }
        private StatUnits HealthUnit { get; set; }
        private StatUnits ManaUnit { get; set; }
        private StatUnits RestUnit { get; set; }
        private StatUnits ResistanceUnit { get; set; }
        private Character Character { get; set; }

        public LevelingManager(Character character)
        {
            StrengthMage = new StatUnits(5, 11, 75, 7);
            DefAgi = new StatUnits(30, 40, 25, 8, unit: 0.75);
            HealthUnit = new StatUnits(30, 40, 25, 8, unit: 1.5);
            ManaUnit = new StatUnits(30, 40, 25, 8, unit: 1.35);
            RestUnit = new StatUnits(24, 34, 25, 15);
            ResistanceUnit = new StatUnits(29, 39, 20, 10);
            Character = character;
        }

        public void LevelUp()
        {
            ++Character.EntityStats.Level;
            string updatedStats = null;

            Stats warrior = Json.GetClassStats(Character.ClassName.ToString());
            PropertyInfo[] warriorStats = Utils.GetProperties(warrior);
            Type type = Character.EntityStats.GetType();

            for (int i = 0; i < warriorStats.Length; ++i)
            {
                PropertyInfo property = warriorStats[i];
                string statName = property.Name;
                if (statName != "Experiences" && statName != "Level" && statName != "Branch" && statName != "MaxHealth" && statName != "MaxMana")
                {
                    double stat = (double)property.GetValue(warrior);

                    PropertyInfo propertyInfo = type.GetProperty(statName);
                    double currentStat = (double)propertyInfo.GetValue(Character.EntityStats);
                    double addStat = IncreaseStat(stat, statName);
                    propertyInfo.SetValue(Character.EntityStats, currentStat + addStat);
                    if (statName == "Health")
                    {
                        Character.EntityStats.MaxHealth += (int)addStat;
                    }
                    else if (statName == "Mana")
                    {
                        Character.EntityStats.MaxMana += (int)addStat;
                    }

                    updatedStats = (updatedStats == null) ? "" : updatedStats;

                    updatedStats += $"{statName}:{(int)addStat},";
                }
            }

            Character.UpdatedStats = updatedStats;
        }

        private int IncreaseStat(double stat, string statName)
        {
            StatUnits concernedUnit;
            if (statName == "Deftness" || statName == "Agility")
            {
                concernedUnit = DefAgi;
            }
            else if (statName == "Health")
            {
                concernedUnit = HealthUnit;
            }
            else if (statName == "Mana")
            {
                concernedUnit = ManaUnit;
            }
            else if (statName == "Resistance")
            {
                concernedUnit = ResistanceUnit;
            }
            else
            {
                if (Character.EntityStats.Branch == "mage" && statName == "Strength")
                {
                    concernedUnit = StrengthMage;
                }
                else
                {
                    concernedUnit = RestUnit;
                }
            }

            stat = RandomizeStat(concernedUnit, (int)stat, statName);
            stat *= concernedUnit.Unit;
            return (int)stat;
        }

        private int RandomizeStat(StatUnits units, int stat, string statName)
        {
            (int unit, string name)[] sortedUnits = units.Units.OrderBy(unit => unit.unit).ToArray();

            int random = RandomNumber.Between(0, 100);
            bool done = false;
            int percent = 0;

            for (int i = 0; i < sortedUnits.Length; ++i)
            {
                if (random <= (percent += sortedUnits[i].unit) && !done)
                {
                    string dataStatName = sortedUnits[i].name;
                    done = true;
                    Regex regex = new Regex(@"[0-9]");
                    if (dataStatName.IndexOf("Minus") >= 0)
                    {
                        int number = int.Parse(regex.Match(dataStatName).Value);
                        stat -= number;
                    }
                    else if (dataStatName.IndexOf("Plus") >= 0)
                    {
                        int number = int.Parse(regex.Match(dataStatName).Value);
                        stat += number;
                    }
                }
            }
            return (stat >= 0) ? stat : 0;
        }
    }
}

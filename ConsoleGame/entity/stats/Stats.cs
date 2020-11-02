namespace ConsoleGame.entity.stats
{
    public class Stats
    {
        public double MaxHealth { get; set; }
        public double Health { get; set; }
        public double MaxMana { get; set; }
        public double Mana { get; set; }
        public double Strength { get; set; }
        public double Resistance { get; set; }
        public double MagicalMight { get; set; }
        public double MagicalMending { get; set; }
        public double Agility { get; set; }
        public double Deftness { get; set; }

        public double this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "MaxHealth":
                        return MaxHealth;
                    case "Health":
                        return Health;
                    case "MaxMana":
                        return MaxMana;
                    case "Mana":
                        return Mana;
                    case "Strength":
                        return Strength;
                    case "Resistance":
                        return Resistance;
                    case "MagicalMight":
                        return MagicalMight;
                    case "MagicalMending":
                        return MagicalMending;
                    case "Agility":
                        return Agility;
                    case "Deftness":
                        return Deftness;
                    default:
                        return 0;
                }
            }
        }

        public Stats(
            double health = 0,
            double mana = 0,
            double strength = 0,
            double resistance = 0,
            double magicalMight = 0,
            double magicalMending = 0,
            double agility = 0,
            double deftness = 0
        )
        {
            MaxHealth = health;
            Health = health;
            MaxMana = mana;
            Mana = mana;
            Strength = strength;
            Resistance = resistance;
            MagicalMight = magicalMight;
            MagicalMending = magicalMending;
            Agility = agility;
            Deftness = deftness;
        }

        public void Deconstruct(out double strength, out double magicalMight)
        {
            strength = Strength;
            magicalMight = MagicalMight;
        }

        public void Deconstruct(
            out double health,
            out double mana,
            out double strength,
            out double resistance,
            out double magicalMight,
            out double magicalMending,
            out double agility,
            out double deftness
        )
        {
            health = Health;
            mana = Mana;
            strength = Strength;
            resistance = Resistance;
            magicalMight = MagicalMight;
            magicalMending = MagicalMending;
            agility = Agility;
            deftness = Deftness;
        }

        public virtual void Init()
        {
            MaxHealth = Health;
            MaxMana = Mana;
        }
    }
}

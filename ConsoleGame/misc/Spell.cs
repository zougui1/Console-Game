namespace ConsoleGame.misc
{
    public class Spell
    {
        /// <summary>
        /// Name represent the spell name
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// RequiredMana represent the mana required to cast the spell
        /// </summary>
        public int RequiredMana { get; protected set; }
        /// <summary>
        /// Power represent the power of the spell
        /// </summary>
        public int Power { get; protected set; }
        /// <summary>
        /// Type represent the type of the spell (attack, heal)
        /// </summary>
        public string Category { get; protected set; }

        public Spell(string name, int requiredMana, int power, string category)
        {
            Name = name;
            RequiredMana = requiredMana;
            Power = power;
            Category = category;
        }
    }
}

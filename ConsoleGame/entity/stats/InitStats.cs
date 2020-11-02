namespace ConsoleGame.entity.stats
{
    public class InitStats : EntityStats
    {
        public int InitHealth { get; set; }
        public int InitMana { get; set; }

        public override void Init()
        {
            Health = InitHealth;
            MaxHealth = InitHealth;
            Mana = InitMana;
            MaxMana = InitMana;
        }
    }
}

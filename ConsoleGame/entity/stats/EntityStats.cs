namespace ConsoleGame.entity.stats
{
    public class EntityStats : Stats
    {
        public int Level { get; set; }
        public int Experiences { get; set; }
        public string Branch { get; set; }

        public EntityStats() : base()
        {
            Level = 1;
            Experiences = 0;
            Branch = null;
        }
    }
}


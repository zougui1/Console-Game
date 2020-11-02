namespace ConsoleGame
{
    public class StatUnits
    {
        public double Unit { get; private set; }
        /*public int Minus2 { get; private set; }
        public int Minus1 { get; private set; }
        public int None { get; private set; }
        public int Plus1 { get; private set; }
        public int Plus2 { get; private set; }*/
        public (int unit, string name)[] Units { get; private set; }

        public StatUnits(int minus2, int minus1, int none, int plus1, int plus2 = 2, double unit = 1)
        {
            /*Minus2 = minus2;
            Minus1 = minus1;
            None = none;
            Plus1 = plus1;
            Plus2 = plus2;
            Unit = unit;*/
            Units = new (int unit, string name)[5]
            {
                (minus2, "Minus2"),
                (minus1, "Minus1"),
                (none, "None"),
                (plus1, "Plus1"),
                (plus2, "Plus2"),
            };
        }
    }
}

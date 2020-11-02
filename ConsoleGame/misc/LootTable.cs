namespace ConsoleGame.misc
{
    public struct LootTable
    {
        public double Percent { get; set; }
        public int Id { get; set; }
        public string DataType { get; set; }

        public void Deconstruct(out double percent, out int id, out string dataType)
        {
            percent = Percent;
            id = Id;
            dataType = DataType;
        }
    }
}

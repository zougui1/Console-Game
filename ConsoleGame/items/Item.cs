using System.Text;

namespace ConsoleGame.items
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Coins { get; set; }
        public StringBuilder ListItemText { get; protected set; } = new StringBuilder();
        public bool IsHeal { get; set; } = false;
        public bool IsMana { get; set; } = false;
        public int Restore { get; set; } = 0;

        public Item(string name, string description, int coins = 0)
        {
            Name = name;
            Description = description;
            Coins = coins;

            ListItemText.Append(Name.PadRight(36));
            ListItemText.AppendFormat("{0} GP", Coins);
        }

        public override string ToString()
        {
            return ListItemText.ToString();
        }

        public virtual void Display(string color = "White")
        {
            ListItemText.Append(Name.PadRight(36));
            ListItemText.AppendFormat("{0} GP", Coins);
        }
    }
}

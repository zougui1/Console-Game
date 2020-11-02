using System.Text;

namespace ConsoleGame.items
{
    public class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Coins { get; protected set; }
        public StringBuilder ListItemText { get; protected set; } = new StringBuilder();
        public bool IsHeal { get; set; } = false;
        public int Restore { get; set; } = 0;

        public Item(string name, string description, int coins = 0)
        {
            Name = name;
            Description = description;
            Coins = coins;

            ListItemText.Append(Name.PadRight(36));
            ListItemText.AppendFormat("{0} GP", Coins);
            ListItemText.AppendLine();
        }

        public override string ToString()
        {
            return ListItemText.ToString();
        }

        public virtual void Display(string color = "White")
        {
            ListItemText.Append(Name.PadRight(36));
            ListItemText.AppendFormat("{0} GP", Coins);
            /*Utils.Cconsole.Color(color).Write(Name.PadRight(36));
            Utils.Cconsole.Color(color).WriteLine("{0} GP", Coins);*/
        }
    }
}

namespace Player
{
    public class Item
    {
        public enum ItemType
        {
            DamageUp,
            DamageDown,
            HealthUp,
            HealthDown,
            LuckUp,
            LuckDown,
            ResistanceUp,
            ResistanceDown,
            HealthPickUp,
            Coins
        }

        public ItemType itemType;
        public int amount;
    }
}
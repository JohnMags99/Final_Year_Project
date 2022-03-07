using UnityEngine;

namespace Player
{
    public class Item : MonoBehaviour
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
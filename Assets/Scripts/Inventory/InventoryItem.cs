using UnityEngine;

namespace FarmGame
{
    [System.Serializable]
    public class InventoryItem
    {
        public Item item;
        public int amount;

        public InventoryItem(Item item)
        {
            this.item = item;
            this.amount = 1;
        }
    }
}

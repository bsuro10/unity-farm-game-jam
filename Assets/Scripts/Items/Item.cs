using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;

        public virtual void Use()
        {
            Debug.Log("Using Item: " + name);
        }

        public void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
        }
    }
}
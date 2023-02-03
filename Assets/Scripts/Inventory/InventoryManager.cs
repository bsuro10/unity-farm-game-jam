using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class InventoryManager : MonoBehaviour
    {
        #region Singleton
        public static InventoryManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        [SerializeField] private int slots = 20;
        [SerializeField] public List<InventoryItem> items;

        private void Start()
        {
            items = new List<InventoryItem>();
        }

        public bool Add(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= slots)
                {
                    Debug.Log("Not enough room in inventory");
                    return false;
                }

                int inventoryItemIndex = items.FindIndex(inventoryItem => inventoryItem.item.Equals(item));
                if (inventoryItemIndex != -1)
                    items[inventoryItemIndex].amount++;
                else
                    items.Add(new InventoryItem(item)); 

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

            }
            return true;
        }

        public void Remove(Item item)
        {
            int inventoryItemIndex = items.FindIndex(inventoryItem => inventoryItem.item.Equals(item));
            if (inventoryItemIndex != -1)
            {
                items[inventoryItemIndex].amount--;
                if (items[inventoryItemIndex].amount == 0)
                    items.RemoveAt(inventoryItemIndex);
            }
            
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

    }
}

using UnityEngine;

namespace FarmGame
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform itemsParent;
        [SerializeField] private GameObject inventoryUI;

        private InventoryManager inventoryManager;
        private InventorySlotUI[] slots;

        void Start()
        {
            inventoryManager = InventoryManager.Instance;
            inventoryManager.onItemChangedCallback += UpdateUI;
            slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
            }
        }

        void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventoryManager.items.Count)
                {
                    InventoryItem inventoryItem = inventoryManager.items[i];
                    slots[i].AddItem(inventoryItem.item, inventoryItem.amount);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

    }
}

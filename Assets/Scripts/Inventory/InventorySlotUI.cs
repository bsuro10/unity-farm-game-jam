using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FarmGame
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amount;

        private Item item;

        public void AddItem(Item newItem, int newAmount)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            amount.text = newAmount.ToString();
            amount.enabled = true;
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            amount.text = null;
            amount.enabled = false;
        }

        public void UseItem()
        {
            if (item != null)
                item.Use();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FarmGame
{
    public class ToolSlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI keyIndex;

        private Tool tool;

        public void AddTool(Tool newTool)
        {
            tool = newTool;
            icon.sprite = tool.icon;
            icon.enabled = true;
        }

        public void ClearSlot()
        {
            tool = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void InitKeyIndex(int index)
        {
            keyIndex.text = index.ToString();
        }
    }
}

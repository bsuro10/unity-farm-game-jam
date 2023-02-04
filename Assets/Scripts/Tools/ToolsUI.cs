using UnityEngine;

namespace FarmGame
{
    public class ToolsUI : MonoBehaviour
    {
        private const int ALPHA_KEY_OFFSET = 49;

        [SerializeField] private Transform toolsParent;

        private ToolsManager toolsManager;
        private ToolSlotUI[] slots;

        void Start()
        {
            toolsManager = ToolsManager.Instance;
            toolsManager.onToolsChanged += UpdateUI;
            slots = toolsParent.GetComponentsInChildren<ToolSlotUI>();
            InitKeyIndexForToolSlots();
        }

        private void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < toolsManager.tools.Count)
                {
                    slots[i].AddTool(toolsManager.tools[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        private void InitKeyIndexForToolSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].InitKeyIndex(i + 1);
            }
        }

    }
}
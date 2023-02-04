using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class ToolsManager : MonoBehaviour
    {
        #region Singleton
        public static ToolsManager Instance { get; private set; }

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

        private const int ALPHA_KEY_OFFSET = 49;

        public delegate void OnToolsChanged();
        public OnToolsChanged onToolsChanged;

        [SerializeField] private int slots = 5;
        [SerializeField] public List<Tool> tools;

        private void Start()
        {
            tools = new List<Tool>();
        }

        void Update()
        {
            for (int i = 0; i < tools.Count; i++)
            {
                InitInputForKeyIndex(i);
            }
        }

        public bool Equip(Tool tool)
        {
            if (tools.Count >= slots)
            {
                Debug.Log("Not enough room in tools inventory");
                return false;
            }
            tools.Add(tool);
            
            if (onToolsChanged != null)
                onToolsChanged.Invoke();

            return true;
        }

        public void Unequip(Tool tool)
        {
            tools.Remove(tool);

            if (onToolsChanged != null)
                onToolsChanged.Invoke();
        }

        private void InitInputForKeyIndex(int index)
        {
            KeyCode indexKeyCode = (KeyCode)(index + ALPHA_KEY_OFFSET);
            if (Input.GetKeyDown(indexKeyCode))
            {
                int toolSlotPressed = ((int)indexKeyCode) - ALPHA_KEY_OFFSET;
                tools[toolSlotPressed].UseTool();
            }
        }
    }
}

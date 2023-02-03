using UnityEngine;

namespace FarmGame
{
    public abstract class Tool : Item
    {
        public override void Use()
        {
            base.Use();
            ToolsManager.Instance.Equip(this);
            RemoveFromInventory();
        }

        public virtual void UseTool()
        {
            Debug.Log("Using tool: " + name);
        }
    }
}

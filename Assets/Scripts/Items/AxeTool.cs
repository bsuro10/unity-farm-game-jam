using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Axe Tool", menuName = "Inventory/New Axe Tool")]
    public class AxeTool : Tool
    {
        public override void UseTool()
        {
            base.UseTool();
            Debug.Log("Using the Axe");
        }
    }
}
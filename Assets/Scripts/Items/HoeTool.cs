using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Hoe Tool", menuName = "Inventory/New Hoe Tool")]
    public class HoeTool : Tool
    {
        public override void UseTool()
        {
            base.UseTool();
            Vector3 playerPosition = PlayerController.Instance.playerTransform.position;
            CropsManager.Instance.PlowGround(playerPosition);
        }
    }
}
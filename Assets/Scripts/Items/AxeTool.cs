using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Axe Tool", menuName = "Inventory/New Axe Tool")]
    public class AxeTool : Tool
    {
        public override void UseTool()
        {
            base.UseTool();
            Vector3 playerPosition = PlayerController.Instance.playerTransform.position;
            if (TileManager.Instance.IsInteractable(playerPosition))
            {
                // TODO: Create a cropsManager to set the tiles related to crops
                //TileManager.Instance.SetInteractedTile(playerPosition);
            }

        }
    }
}
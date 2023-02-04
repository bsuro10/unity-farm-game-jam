using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Seed", menuName = "Inventory/New Seed")]
    public class Seed : Item
    {
        public Crop crop;

        public override void Use()
        {
            base.Use();
            Vector3 playerPosition = PlayerController.Instance.playerTransform.position;
            if (CropsManager.Instance.Seed(playerPosition, crop))
                RemoveFromInventory();
        }
    }
}

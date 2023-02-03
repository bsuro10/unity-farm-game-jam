using UnityEngine;

namespace FarmGame
{
    public class Collectible : Interactable
    {
        [SerializeField] private Item item;

        public override void Interact()
        {
            base.Interact();
            PickUp();
        }

        private void PickUp()
        {
            Debug.Log("Picking up " + item.name);
            bool wasPickedUp = InventoryManager.Instance.Add(item);

            if (wasPickedUp)
                Destroy(gameObject);
        }

    }
}

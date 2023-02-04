using UnityEngine;

namespace FarmGame
{
    public class Collectible : Interactable
    {
        [SerializeField] private Item item;
        [SerializeField] private int amount = 1;

        public override void Interact()
        {
            base.Interact();
            PickUp();
        }

        public void SetCollectibeItem(Item item, int amount)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            this.item = item;
            this.amount = amount;
            renderer.sprite = item.icon;
        }

        private void PickUp()
        {
            Debug.Log("Picking up " + amount + " " + item.name);
            bool wasPickedUp = InventoryManager.Instance.Add(item, amount);

            if (wasPickedUp)
                Destroy(gameObject);
        }

    }
}

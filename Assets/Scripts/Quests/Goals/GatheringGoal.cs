using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class GatheringGoal : QuestGoal
    {
        [SerializeField] private Item itemToGather;
        [SerializeField] private bool removeItemUponCompletion;

        public override string GetDescription()
        {
            return $"Gather {requiredAmount} {itemToGather.name}";
        }

        public override void Initialize()
        {
            base.Initialize();
            CheckIfAlreadyGathered();
            InventoryManager.Instance.onItemChangedCallback += OnItemChanged;
        }

        public override void CompleteGoal()
        {
            base.CompleteGoal();
            InventoryManager.Instance.onItemChangedCallback -= OnItemChanged;
            if (removeItemUponCompletion)
                InventoryManager.Instance.Remove(itemToGather, requiredAmount);
        }

        public void OnItemChanged()
        {
            CheckIfAlreadyGathered();
        }

        private void CheckIfAlreadyGathered()
        {
            List<InventoryItem> inventoryItems = InventoryManager.Instance.items;
            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                if (inventoryItem.item.name.Equals(itemToGather.name))
                {
                    currentAmount = inventoryItem.amount;
                    Evaluate();
                    break;
                }
            }
        }
    }
}
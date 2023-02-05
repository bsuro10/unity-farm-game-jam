using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(menuName = "Quests/New Gathering Goal", fileName = "New Gathering Goal")]
    public class GatheringGoal : QuestGoal
    {
        public Item itemToGather;

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
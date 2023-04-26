using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FarmGame
{
    public class RewardSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemTitleText;

        public void InitializeRewardSlot(Item item)
        {
            itemTitleText.text = item.name;
        }
    }
}
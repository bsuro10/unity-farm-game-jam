using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FarmGame
{
    public class GoalSlotUI : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI goalTitleText;
        [SerializeField] private TextMeshProUGUI goalAmountText;

        public void InitializeGoalSlot(QuestGoal questGoal)
        {
            goalTitleText.text = questGoal.GetDescription();
            goalAmountText.text = $"{questGoal.currentAmount}/{questGoal.requiredAmount}";
        }
        
    }
}
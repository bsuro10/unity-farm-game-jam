using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FarmGame
{
    public class QuestSlotUI : MonoBehaviour
    {
        public delegate void OnQuestSlotUIClicked(Quest quest);
        public OnQuestSlotUIClicked onQuestSlotUIClicked;

        [SerializeField] private TextMeshProUGUI questTitleText;

        public Quest quest { get; private set; }

        public void InitializeQuestSlot(Quest quest)
        {
            this.quest = quest;
            questTitleText.text = this.quest.questData.information.name;
        }

        public void QuestSlotUIClicked()
        {
            if (onQuestSlotUIClicked != null)
                onQuestSlotUIClicked.Invoke(quest);
        }
    }
}
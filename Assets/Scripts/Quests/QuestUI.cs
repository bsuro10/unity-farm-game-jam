using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FarmGame {
    public class QuestUI : MonoBehaviour
    {

        [SerializeField] private GameObject questsPanelUI;
        [SerializeField] private GameObject questItemPrefab;
        [SerializeField] private Transform questItemListParent;
        [SerializeField] private GameObject goalItemPrefab;
        [SerializeField] private Transform goalItemListParent;
        [SerializeField] private TextMeshProUGUI questDetailsTitle;
        [SerializeField] private TextMeshProUGUI questDetailsDescription;

        private QuestManager questManager;
        private string selectedQuestId;

        void Start()
        {
            questManager = QuestManager.Instance;
            questManager.onQuestAdded += AddQuestItem;
            ClearQuestSlotDetails();
        }

        void Update()
        {
            if (Input.GetButtonDown("Quests"))
            {
                questsPanelUI.SetActive(!questsPanelUI.activeSelf);
                if (!questsPanelUI.activeSelf)
                    ClearQuestSlotDetails();
            }
        }

        public void DisplayQuestSlotDetails(Quest quest)
        {
            if (selectedQuestId == null || selectedQuestId != quest.questData.id)
            {
                ClearQuestSlotDetails();
                selectedQuestId = quest.questData.id;
                questDetailsTitle.text = quest.questData.information.name;
                questDetailsDescription.text = quest.questData.information.description;
                foreach (QuestGoal questGoal in quest.questData.goals)
                {
                    GameObject goalItem = Instantiate(goalItemPrefab, goalItemListParent);
                    GoalSlotUI goalSlotUI = goalItem.GetComponent<GoalSlotUI>();
                    if (goalSlotUI != null)
                        goalSlotUI.InitializeGoalSlot(questGoal);
                }
            }
        }

        public void ClearQuestSlotDetails()
        {
            foreach (Transform goalItem in goalItemListParent.GetComponentInChildren<Transform>())
            {
                Destroy(goalItem.gameObject);
            }
            questDetailsTitle.text = "";
            questDetailsDescription.text = "";
            selectedQuestId = null;
        }

        private void AddQuestItem(string questId, Quest quest)
        {
            GameObject questItem = Instantiate(questItemPrefab, questItemListParent);
            QuestSlotUI questSlotUI = questItem.GetComponent<QuestSlotUI>();
            if (questSlotUI != null)
            {
                questSlotUI.InitializeQuestSlot(quest);
                questSlotUI.onQuestSlotUIClicked += DisplayQuestSlotDetails;
            }
        }

    }
}

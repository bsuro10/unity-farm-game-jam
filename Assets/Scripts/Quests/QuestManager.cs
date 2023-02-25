using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class QuestManager : MonoBehaviour
    {
        #region Singleton
        public static QuestManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        private Dictionary<string, Quest> activeQuests;
        private Dictionary<string, Quest> completedQuests;

        private void Start()
        {
            activeQuests = new Dictionary<string, Quest>();
            completedQuests = new Dictionary<string, Quest>();
        }

        public void AddQuest(Quest quest)
        {
            QuestData questData = quest.questData;
            if (CanQuestBeAdded(questData))
            {
                activeQuests.Add(questData.id, quest);
                activeQuests[questData.id].onQuestCompleted.AddListener(OnQuestCompleted);
                Debug.Log("Adding a new quest: " + questData.name);
            }
        }

        private void OnQuestCompleted(Quest quest)
        {
            // TODO: Give reward to player, or not?
            QuestData questData = quest.questData;
            activeQuests.Remove(questData.id);
            completedQuests.Add(questData.id, quest);
            Debug.Log("Completed the quest: " + questData.name);
        }

        public bool CanQuestBeAdded(QuestData questData)
        {
            if (activeQuests.ContainsKey(questData.id) || completedQuests.ContainsKey(questData.id))
                return false;

            return isQuestPrerequisiteCompleted(questData);
        }

        private bool isQuestPrerequisiteCompleted(QuestData questData)
        {
            foreach (QuestData prerequisiteQuest in questData.prerequisitesQuests)
            {
                if (!completedQuests.ContainsKey(prerequisiteQuest.id))
                    return false;
            }
            return true;
        }
    }
}
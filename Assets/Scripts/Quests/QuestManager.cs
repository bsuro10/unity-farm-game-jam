using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FarmGame
{
    public class QuestAdded : UnityEvent<string, Quest> { }

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

        public delegate void OnQuestAdded(string questId, Quest quest);
        public OnQuestAdded onQuestAdded;

        public Dictionary<string, Quest> activeQuests { get; private set; }
        public Dictionary<string, Quest> completedQuests { get; private set; }

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
                
                if (onQuestAdded != null)
                    onQuestAdded.Invoke(questData.id, quest);
            }
        }

        private void OnQuestCompleted(Quest quest)
        {
            QuestData questData = quest.questData;
            questData.reward.items.ForEach(item => InventoryManager.Instance.Add(item));
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
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
            if (CanQuestBeAdded(quest))
            {
                quest.Initialize();
                activeQuests.Add(quest.id, quest);
                activeQuests[quest.id].OnQuestCompleted.AddListener(OnQuestCompleted);
                Debug.Log("Adding a new quest: " + quest.name);
            }
        }

        private void OnQuestCompleted(Quest quest)
        {
            // TODO: Give reward to player
            activeQuests.Remove(quest.id);
            completedQuests.Add(quest.id, quest);
            Debug.Log("Completed the quest: " + quest.name);
        }

        public bool CanQuestBeAdded(Quest quest)
        {
            if (activeQuests.ContainsKey(quest.id) || completedQuests.ContainsKey(quest.id))
                return false;

            return isQuestPrerequisiteCompleted(quest);
        }

        private bool isQuestPrerequisiteCompleted(Quest quest)
        {
            foreach (Quest prerequisiteQuest in quest.prerequisitesQuests)
            {
                if (!completedQuests.ContainsKey(prerequisiteQuest.name))
                    return false;
            }
            return true;
        }
    }
}
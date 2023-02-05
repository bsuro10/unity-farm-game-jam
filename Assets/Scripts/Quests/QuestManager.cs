using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class QuestManager : MonoBehaviour
    {
        public Quest currentActiveQuest;
        public List<Quest> questsHistory;

        private void Start()
        {
            // TODO: Debugging
            if (currentActiveQuest != null)
            {
                currentActiveQuest.Initialize();
                currentActiveQuest.OnQuestCompleted.AddListener(OnQuestCompleted);
            }
        }

        public void AddQuest(Quest quest)
        {
            if (currentActiveQuest == null)
            {
                currentActiveQuest = quest;
                currentActiveQuest.Initialize();
                currentActiveQuest.OnQuestCompleted.AddListener(OnQuestCompleted);
            }
        }

        public void OnQuestCompleted(Quest quest)
        {
            if (currentActiveQuest != null)
            {
                questsHistory.Add(currentActiveQuest);
                currentActiveQuest.OnQuestCompleted.RemoveAllListeners();
                currentActiveQuest = null;
            }
        }
    }
}
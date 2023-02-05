using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FarmGame
{
    public class QuestCompletedEvent : UnityEvent<Quest> { }

    [CreateAssetMenu(menuName = "Quests/New Quest", fileName = "New Quest")]
    public class Quest : ScriptableObject
    {
        [System.Serializable]
        public struct Info
        {
            public string name;
            public string description;
        }

        [Header("Info")]
        public Info information;

        [System.Serializable]
        public struct Reward
        {
            public int exp;
            public int currency;
            public List<Item> items;
        }

        [Header("Reward")]
        public Reward reward;

        [Header("Goals")]
        public List<QuestGoal> goals;

        public bool isCompleted { get; protected set; }
        public QuestCompletedEvent OnQuestCompleted;

        public void Initialize()
        {
            isCompleted = false;
            OnQuestCompleted = new QuestCompletedEvent();
            foreach (QuestGoal goal in goals)
            {
                goal.Initialize();
                goal.OnGoalCompleted.AddListener(delegate { CheckGoals(); });
            }
        }

        private void CheckGoals()
        {
            isCompleted = goals.All(g => g.isCompleted);
            if (isCompleted)
            {
                // Give reward
                OnQuestCompleted.Invoke(this);
                OnQuestCompleted.RemoveAllListeners();
            }
        }
    }
}
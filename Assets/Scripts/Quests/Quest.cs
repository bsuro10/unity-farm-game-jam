using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace FarmGame
{
    public class QuestCompletedEvent : UnityEvent<Quest> { }

    [CreateAssetMenu(menuName = "Quests/New Quest", fileName = "New Quest")]
    public class Quest : ScriptableObjectWithIdAttribute
    {
        [Serializable]
        public struct Info
        {
            public string name;
            public string description;
        }

        [Header("Info")]
        public Info information;

        [Serializable]
        public struct Reward
        {
            public int currency;
            public List<Item> items;
        }

        [Header("Reward")]
        public Reward reward;

        [Header("Goals")]
        public List<QuestGoal> goals;

        [Header("Prerequisites")]
        public List<Quest> prerequisitesQuests;

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

        public void CompleteQuest()
        {
            OnQuestCompleted.Invoke(this);
            OnQuestCompleted.RemoveAllListeners();
        }

        private void CheckGoals()
        {
            isCompleted = goals.All(g => g.isCompleted);
        }

    }
}
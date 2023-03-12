using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FarmGame
{
    public class QuestCompletedEvent : UnityEvent<Quest> { }

    public class Quest
    {
        public delegate void OnQuestUpdated(Quest quest);
        public OnQuestUpdated onQuestUpdated;

        public QuestData questData { get; private set; }
        public QuestStatus questStatus { get; private set; }
        public QuestCompletedEvent onQuestCompleted { get; private set; }

        public Quest(QuestData questData)
        {
            this.questData = questData;
            questStatus = QuestStatus.Started;
            onQuestCompleted = new QuestCompletedEvent();
            foreach (QuestGoal goal in questData.goals)
            {
                goal.onGoalStatusChanged.AddListener(delegate { CheckGoals(); });
                goal.Initialize();
            }
        }

        public void CompleteQuest()
        {
            foreach (QuestGoal goal in questData.goals)
            {
                goal.CompleteGoal();
            }
            onQuestCompleted.Invoke(this);
            onQuestCompleted.RemoveAllListeners();
        }

        private void CheckGoals()
        {
            if (questData.goals.All(g => g.goalStatus == GoalStatus.Completed))
            {
                questStatus = QuestStatus.Completed;
            }
            else 
            {
                questStatus = QuestStatus.Started;
            }

            if (onQuestUpdated != null)
                onQuestUpdated.Invoke(this);
        }
    }

    public enum QuestStatus { None, Started, Completed }
}
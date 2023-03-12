using UnityEngine;
using UnityEngine.Events;

namespace FarmGame
{
    public class GoalStatusChangedEvent : UnityEvent { }

    [System.Serializable]
    public abstract class QuestGoal
    {
        [SerializeField] public int requiredAmount;

        public GoalStatusChangedEvent onGoalStatusChanged { get; protected set; }
        public GoalStatus goalStatus { get; protected set; }

        protected string description;
        public int currentAmount { get; protected set; }

        public QuestGoal()
        {
            goalStatus = GoalStatus.Inprogress;
            onGoalStatusChanged = new GoalStatusChangedEvent();
        }

        public virtual string GetDescription() { return description; }

        public virtual void Initialize() { }

        public virtual void CompleteGoal ()
        {
            onGoalStatusChanged.RemoveAllListeners();
        }

        protected void Evaluate()
        {
            if (currentAmount >= requiredAmount)
            {
                goalStatus = GoalStatus.Completed;
            }
            else
            {
                goalStatus = GoalStatus.Inprogress;
            }
            onGoalStatusChanged.Invoke();
            Debug.Log("Goal Status: " + goalStatus);
        }
    }
}

public enum GoalStatus { Inprogress, Completed }
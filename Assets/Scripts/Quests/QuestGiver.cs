using UnityEngine;

namespace FarmGame
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private QuestData questData;
        [SerializeField] private DialogueData questStartDialogueData;
        [SerializeField] private DialogueData questInProgressDialogueData;
        [SerializeField] private DialogueData questCompletedDialogueData;

        private Quest quest;

        private void Start()
        {
            
        }

        public bool canQuestBeAdded
        {
            get
            {
                return QuestManager.Instance.CanQuestBeAdded(questData);
            }
        }

        public bool InteractWithQuest()
        {
            bool wasInteractingWithQuest = true;
            if (canQuestBeAdded)
            {
                GiveQuest();
            }
            else if ((quest != null) && (quest.questStatus == QuestStatus.Started))
            {
                ShowInProgressDialogue();
            }
            else if ((quest != null) && (quest.questStatus == QuestStatus.Completed))
            {
                ShowCompletedDialogue();
            }
            else
            {
                wasInteractingWithQuest = false;
            }
            return wasInteractingWithQuest;
        }

        private void GiveQuest()
        {
            quest = new Quest(questData);
            if (questStartDialogueData)
            {
                DialogueManager.Instance.StartDialogue(questStartDialogueData, delegate { StartQuest(); });
            }
            else
            {
                StartQuest();
            }
        }

        private void StartQuest()
        {
            if (quest != null)
            {
                QuestManager.Instance.AddQuest(quest);
            }
        }

        private void ShowCompletedDialogue()
        {
            if (questCompletedDialogueData)
            {
                DialogueManager.Instance.StartDialogue(questCompletedDialogueData, delegate { CompleteQuest(); });
            }
            else
            {
                CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            quest.CompleteQuest();
            quest = null;
        }

        private void ShowInProgressDialogue()
        {
            if (questInProgressDialogueData)
            {
                DialogueManager.Instance.StartDialogue(questInProgressDialogueData, null);
            }
        }

    }
}
using UnityEngine;

namespace FarmGame
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] public QuestData questData;

        private Quest quest;
        private CharacterBasicController characterBasicController;

        private void Start()
        {
            characterBasicController = GetComponent<CharacterBasicController>();
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
            if ((questData != null) && (canQuestBeAdded))
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
            if (questData.questStartDialogueData)
            {
                DialogueManager.Instance.StartDialogue(questData.questStartDialogueData, delegate { StartQuest(); }, characterBasicController);
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
            if (questData.questCompletedDialogueData)
            {
                DialogueManager.Instance.StartDialogue(questData.questCompletedDialogueData, delegate { CompleteQuest(); }, characterBasicController);
            }
            else
            {
                CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            quest.CompleteQuest();
            if (quest.questData.nextQuest != null)
                questData = quest.questData.nextQuest;
            quest = null;
        }

        private void ShowInProgressDialogue()
        {
            if (questData.questInProgressDialogueData)
            {
                DialogueManager.Instance.StartDialogue(questData.questInProgressDialogueData, null, characterBasicController);
            }
        }

    }
}
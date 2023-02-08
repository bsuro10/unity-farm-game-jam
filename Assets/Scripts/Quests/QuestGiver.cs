using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmGame
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private Quest quest;
        [SerializeField] private Dialogue questDialogue;
        [SerializeField] private Dialogue onCompleteQuestDialogue;

        private bool questHaveBeenCompleted = false;

        public bool isQuestAvailable
        {
            get
            {
                return QuestManager.Instance.CanQuestBeAdded(quest);
            }
        }

        public bool InteractWithQuest()
        {
            bool wasInteractingWithQuest = true;
            if (isQuestAvailable)
            {
                GiveQuest();
            }
            else if (quest.isCompleted && !questHaveBeenCompleted)
            {
                CompleteQuest();
            }
            else
            {
                wasInteractingWithQuest = false;
            }
            return wasInteractingWithQuest;
        }

        private void GiveQuest()
        {
            DialogueManager.Instance.StartDialogue(questDialogue);
            questDialogue.onDialogueFinishEvent.AddListener(delegate { QuestManager.Instance.AddQuest(quest); });
        }

        private void CompleteQuest()
        {
            DialogueManager.Instance.StartDialogue(onCompleteQuestDialogue);
            onCompleteQuestDialogue.onDialogueFinishEvent.AddListener(delegate { quest.CompleteQuest(); });
            questHaveBeenCompleted = true;
        }

    }
}
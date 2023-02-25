using UnityEngine;

namespace FarmGame
{
    public class Npc : Interactable
    {
        [SerializeField] private NpcData npcData;
        [SerializeField] private Dialogue dialogue;

        private QuestGiver questGiver;

        private void Start()
        {
            questGiver = GetComponent<QuestGiver>();
        }

        public override void Interact()
        {
            base.Interact();

            if (npcData == null)
                return;

            if (questGiver && questGiver.InteractWithQuest())
            {
                return;
            }
            else if (dialogue)
            {
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }

    }
}

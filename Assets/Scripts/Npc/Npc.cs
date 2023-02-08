using UnityEngine;

namespace FarmGame
{
    public class Npc : Interactable
    {
        [SerializeField] private NpcData npcData;
        [SerializeField] private Dialogue dialogue;

        public override void Interact()
        {
            base.Interact();
            if (npcData != null && dialogue != null)
            {
                DialogueManager.Instance.StartDialogue(npcData, dialogue);
            }
        }

    }
}

using UnityEngine;

namespace FarmGame
{
    [RequireComponent(typeof(Dialogue))]
    public class StartGame : MonoBehaviour
    {

        public Dialogue dialogue;

        private void Start()
        {
            StartDialogue();
        }

        public void StartDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            gameObject.SetActive(false);
        }
    }
}
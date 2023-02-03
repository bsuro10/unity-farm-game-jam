using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace FarmGame
{
    public class DialogueManager : MonoBehaviour
    {
        #region Singleton
        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [Header("UI")]
        [SerializeField] private Animator anim;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProCustom bodyText;

        private Queue<string> sentences;
        private UnityEvent onDialogueFinishEvent;

        void Start()
        {
            sentences = new Queue<string>();
            bodyText.ReadText("");
        }

        public void StartDialogue(NpcData npcData, Dialogue dialogue)
        {
            anim.SetBool("isShown", true);
            nameText.text = npcData.name;
            nameText.color = npcData.nameColor;
            sentences.Clear();
            onDialogueFinishEvent = dialogue.onDialogueFinishEvent;
            PlayerController.Instance.isInDialogue = true;
            foreach (string sentence in dialogue.dialogueData.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            bodyText.ReadText(sentence);
        }

        private void EndDialogue()
        {
            anim.SetBool("isShown", false);
            PlayerController.Instance.isInDialogue = false;
            onDialogueFinishEvent.Invoke();
            bodyText.ReadText("");
        }

        private void DialogueShown_AnimatonEvent()
        {
            DisplayNextSentence();
        }
    }
}
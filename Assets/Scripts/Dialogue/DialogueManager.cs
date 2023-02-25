using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static FarmGame.DialogueData;

namespace FarmGame
{
    public class DialogueManager : MonoBehaviour
    {
        #region Singleton
        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            sentences = new Queue<string>();
            bodyText.ReadText("");

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
        [SerializeField] private NpcData anonNpcData;

        private Queue<string> sentences;
        private UnityEvent onDialogueFinishEvent;

        private void Start()
        {
            onDialogueFinishEvent = new UnityEvent();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            ShowDialogue(dialogue.dialogueData, dialogue.onDialogueFinishEvent);
        }

        public void StartDialogue(DialogueData dialogueData, UnityAction action)
        {
            UnityEvent onDialogueFinishEvent = new UnityEvent();

            if (action != null)
                onDialogueFinishEvent.AddListener(action);

            ShowDialogue(dialogueData, onDialogueFinishEvent);
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

        private void ShowDialogue(DialogueData dialogueData, UnityEvent onDialogueFinishEvent)
        {
            anim.SetBool("isShown", true);
            sentences.Clear();
            this.onDialogueFinishEvent = onDialogueFinishEvent;
            PlayerController.Instance.isInDialogue = true;
            foreach (DialogueSentence dialogueSentence in dialogueData.sentences)
            {
                NpcData npcData = anonNpcData;

                if (dialogueSentence.npcData)
                    npcData = dialogueSentence.npcData;
                
                nameText.text = npcData.name;
                nameText.color = npcData.nameColor;
                sentences.Enqueue(dialogueSentence.sentence);
            }
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
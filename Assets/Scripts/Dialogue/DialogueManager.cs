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
            sentences = new Queue<DialogueSentence>();
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

        private Queue<DialogueSentence> sentences;
        private UnityEvent onDialogueFinishEvent;
        private CharacterBasicController targetNpcController;

        private void Start()
        {
            onDialogueFinishEvent = new UnityEvent();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            ShowDialogue(dialogue.dialogueData, dialogue.onDialogueFinishEvent, null);
        }

        public void StartDialogue(Dialogue dialogue, CharacterBasicController targetNpcController)
        {
            ShowDialogue(dialogue.dialogueData, dialogue.onDialogueFinishEvent, targetNpcController);
        }

        public void StartDialogue(DialogueData dialogueData, UnityAction action, CharacterBasicController targetNpcController)
        {
            UnityEvent onDialogueFinishEvent = new UnityEvent();

            if (action != null)
                onDialogueFinishEvent.AddListener(action);

            ShowDialogue(dialogueData, onDialogueFinishEvent, targetNpcController);
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            NpcData npcData = anonNpcData;
            DialogueSentence dialogueSentence = sentences.Dequeue();
            if (dialogueSentence.npcData != null)
                npcData = dialogueSentence.npcData;

            nameText.text = npcData.name;
            nameText.color = npcData.nameColor;
            bodyText.ReadText(dialogueSentence.sentence);
        }

        private void ShowDialogue(DialogueData dialogueData, UnityEvent onDialogueFinishEvent, CharacterBasicController targetNpcController)
        {
            anim.SetBool("isShown", true);
            sentences.Clear();
            this.onDialogueFinishEvent = onDialogueFinishEvent;
            this.targetNpcController = targetNpcController;
            PlayerController.Instance.isInDialogue = true;

            if (this.targetNpcController != null)
                this.targetNpcController.isInDialogue = true;

            foreach (DialogueSentence dialogueSentence in dialogueData.sentences)
            {
                sentences.Enqueue(dialogueSentence);
            }
        }

        private void EndDialogue()
        {
            anim.SetBool("isShown", false);
            PlayerController.Instance.isInDialogue = false;

            if (targetNpcController != null)
                targetNpcController.isInDialogue = false;

            onDialogueFinishEvent.Invoke();
            bodyText.ReadText("");
            nameText.text = "";
        }

        private void DialogueShown_AnimatonEvent()
        {
            DisplayNextSentence();
        }
    }
}
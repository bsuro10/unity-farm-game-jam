using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FarmGame {
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject blackScreenImage;
        [SerializeField] private QuestData triggeringQuestData;
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private AudioClip sound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && QuestManager.Instance.completedQuests.ContainsKey(triggeringQuestData.id))
            {
                blackScreenImage.SetActive(true);
                SoundManager.Instance.ChangeBackgroundMusic(sound);
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }

        public void LoadStartGameScene()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
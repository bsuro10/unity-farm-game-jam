using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton
        public static SoundManager Instance { get; private set; }

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

        [SerializeField] private AudioSource backgroundMusicSource;

        private AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            source.PlayOneShot(sound);
        }

        public void ChangeBackgroundMusic(AudioClip sound)
        {
            backgroundMusicSource.clip = sound;
            backgroundMusicSource.Play();
        }
    }
}
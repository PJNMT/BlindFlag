using System;
using UnityEngine;
using Random = UnityEngine.Random;


    public class FootStep : MonoBehaviour
    {
        private cubecontroller Player;
        public AudioClip walk;
        private AudioSource _audioSource;
        
        private void Start()
        {
            Player = FindObjectOfType<cubecontroller>();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = walk;
        }

        void Update()
        {
            
        }

        public void Sound()
        {
            if (!_audioSource.isPlaying)
            {
                Debug.Log("play audio");
                float rnd = Random.Range(0.2f, 1f);
                _audioSource.volume = rnd;
                _audioSource.PlayOneShot(walk);

            }
        }
    }

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
            if (!_audioSource.isPlaying && Player.transform.position.magnitude>2)
            {
                _audioSource.volume = Random.Range(0.7f, 1f);
                
                _audioSource.Play();
                
            }
        }
    }

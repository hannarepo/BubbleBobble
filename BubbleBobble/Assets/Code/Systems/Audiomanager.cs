using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Audiomanager : MonoBehaviour
    {
        [Header("------------------- Audio Sources -----------------")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        [Header("------------------- Audio Clips -----------------")]
        public AudioClip backgroundMusic;
        public AudioClip powerUpSFX;
        // add more audio clips here
        // Juho's note: check rehopeT

        private void Start()
        {
            _musicSource.clip = backgroundMusic;
            _musicSource.Play();
        }
    }
}

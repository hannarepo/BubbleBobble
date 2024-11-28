using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace BubbleBobble
{
    public class VolumeSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _SFXSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                LoadVolume();
            }
            else
            {
                SetMusicVolume();
                SetSFXVolume();
            }
        }

        public void SetMusicVolume()
        {
            float volume = _musicSlider.value;
            _audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
        }

        public void SetSFXVolume()
        {
            float volume = _SFXSlider.value;
            _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }

        private void LoadVolume()
        {
            _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

            SetMusicVolume();
            SetSFXVolume();
        }
    }
}

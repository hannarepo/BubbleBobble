using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Audiomanager : MonoBehaviour
    {
		private LevelChanger _levelChanger;
		[SerializeField] private int _underWaterIndex;
		[SerializeField] private int _windowsIndex;
		[SerializeField] private int _liminalIndex;

        [Header("------------------- Audio Sources -----------------")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        [Header("------------------- Audio Clips -----------------")]
        [SerializeField] private AudioClip _backgroundMusic;
		[SerializeField] private AudioClip _underWaterMusic;
		[SerializeField] private AudioClip _windowsMusic;
		[SerializeField] private AudioClip _liminalMusic;
        [SerializeField] private AudioClip _powerUpSFX;
        // add more audio clips here
        // Juho's note: check rehopeT

        private void Start()
        {
			_levelChanger = GetComponent<LevelChanger>();
            _musicSource.clip = _backgroundMusic;
            _musicSource.Play();
        }

		private void Update()
		{
			if (_levelChanger.LevelIndex >= _underWaterIndex && _levelChanger.LevelIndex < _windowsIndex)
			{
				_backgroundMusic = _underWaterMusic;
			}
			else if (_levelChanger.LevelIndex >= _windowsIndex && _levelChanger.LevelIndex < _liminalIndex)
			{
				_backgroundMusic = _windowsMusic;
			}
			else if (_levelChanger.LevelIndex >= _liminalIndex)
			{
				_backgroundMusic = _liminalMusic;
			}
		}
    }
}

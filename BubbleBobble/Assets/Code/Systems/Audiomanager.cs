using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleBobble
{
    public class Audiomanager : MonoBehaviour
    {
		[SerializeField] private float _musicFadeTime = 1f;
		private bool _isPlayingMusicSource1 = true;

        [Header("------------------- Audio Sources -----------------")]
        [SerializeField] private AudioSource _musicSource1;
		[SerializeField] private AudioSource _musicSource2;
        [SerializeField] private AudioSource _sfxSource;

        [Header("------------------- Audio Clips -----------------")]
        [SerializeField] private AudioClip _backgroundMusic;
        [SerializeField] private AudioClip _popSFX;
        // add more audio clips here
        // Juho's note: check rehopeT

        private void Start()
        {
            _musicSource1.clip = _backgroundMusic;

			if (SceneManager.GetActiveScene().name != "Main Menu")
			{
				ChangeMusic(_backgroundMusic);
			}
			else
			{
				_musicSource1.Play();
			}
        }

		public void ChangeMusic(AudioClip audioClip)
		{
			StopAllCoroutines();
			StartCoroutine(FadeMusic(audioClip));
			_isPlayingMusicSource1 = !_isPlayingMusicSource1;
		}

		private IEnumerator FadeMusic(AudioClip musicClip)
		{
			float timeElapsed = 0f;

			if (_isPlayingMusicSource1)
			{
				_musicSource2.clip = musicClip;
				_musicSource2.Play();

				while (timeElapsed < _musicFadeTime)
				{
					_musicSource2.volume = Mathf.Lerp(0, 1, timeElapsed / _musicFadeTime);
					_musicSource1.volume = Mathf.Lerp(1, 0, timeElapsed / _musicFadeTime);
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_musicSource1.Stop();
			}
			else
			{
				_musicSource1.clip = musicClip;
				_musicSource1.Play();

				while (timeElapsed < _musicFadeTime)
				{
					_musicSource1.volume = Mathf.Lerp(0, 1, timeElapsed / _musicFadeTime);
					_musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / _musicFadeTime);
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_musicSource2.Stop();
			}
		}

		public void PlaySFX(AudioClip audioClip)
		{
			_sfxSource.PlayOneShot(audioClip);
		}
    }
}

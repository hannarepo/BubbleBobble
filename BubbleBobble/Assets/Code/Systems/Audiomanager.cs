using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleBobble
{
	public class Audiomanager : MonoBehaviour
	{
		[SerializeField] private float _musicFadeTime = 1f;
		[SerializeField] private float _musicSpeedFadeTime = 0.5f;
		[SerializeField, Tooltip("This fade time is used when game ends and credits are being loaded")]
		private float _musicFadeOutTime = 1f;
		[SerializeField] private float _hurryUpPitch = 140;
		private bool _isPlayingMusicSource1 = true;
		private float _initialPitch = 0;
		private Coroutine _speedChangeCoroutine;

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
			_initialPitch = _musicSource1.pitch;

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

		public void FadeOut()
		{
			StopAllCoroutines();
			StartCoroutine(FadeOutMusic());
		}

		public void SpeedUpMusic()
		{
			if (_speedChangeCoroutine != null)
			{
				StopCoroutine(_speedChangeCoroutine);
			}
			_speedChangeCoroutine = StartCoroutine(FadeMusicSpeedUp());
		}

		public void SlowDownMusic()
		{
			if (_speedChangeCoroutine != null)
			{
				StopCoroutine(_speedChangeCoroutine);
			}
			_speedChangeCoroutine = StartCoroutine(FadeMusicSpeedDown());
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

		private IEnumerator FadeOutMusic()
		{
			float timeElapsed = 0f;

			if (_isPlayingMusicSource1)
			{
				while (timeElapsed < _musicFadeTime)
				{
					_musicSource1.volume = Mathf.Lerp(1, 0, timeElapsed / _musicFadeOutTime);
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_musicSource1.Stop();
			}
			else
			{
				while (timeElapsed < _musicFadeTime)
				{
					_musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / _musicFadeOutTime);
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_musicSource2.Stop();
			}
		}

		private IEnumerator FadeMusicSpeedUp()
		{
			AudioSource musicSource;
			float timeElapsed = 0f;

			if (_isPlayingMusicSource1)
			{
				musicSource = _musicSource1;
			}
			else
			{
				musicSource = _musicSource2;
			}

			while (timeElapsed < _musicSpeedFadeTime)
			{
				musicSource.pitch = Mathf.Lerp(_initialPitch, _hurryUpPitch, timeElapsed / _musicSpeedFadeTime);
				timeElapsed += Time.deltaTime;
				yield return null;
			}
		}


		private IEnumerator FadeMusicSpeedDown()
		{
			AudioSource musicSource;
			float timeElapsed = 0f;

			if (_isPlayingMusicSource1)
			{
				musicSource = _musicSource1;
			}
			else
			{
				musicSource = _musicSource2;
			}

			while (timeElapsed < _musicSpeedFadeTime)
			{
				musicSource.pitch = Mathf.Lerp(_hurryUpPitch, _initialPitch, timeElapsed / _musicSpeedFadeTime);
				timeElapsed += Time.deltaTime;
				yield return null;
			}
		}


		public void PlaySFX(AudioClip audioClip)
		{
			_sfxSource.PlayOneShot(audioClip);
		}
	}
}

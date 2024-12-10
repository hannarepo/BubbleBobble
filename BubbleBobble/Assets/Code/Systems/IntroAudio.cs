using UnityEngine;

namespace BubbleBobble
{
    public class IntroAudio : MonoBehaviour
    {
		private AudioSource _audioSource;

        private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void PlayKeyboardSFX()
		{
			_audioSource.Play();
		}

		private void StopSFX()
		{
			_audioSource.Stop();
		}
    }
}

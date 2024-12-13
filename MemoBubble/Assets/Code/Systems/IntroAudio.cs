using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// For animation event in intro cutscene to play keyboard sfx only in the second part of the cutscene.
	/// </summary>
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

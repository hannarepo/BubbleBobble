using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Music fade in for credit scene.
	/// </summary>
	/// 
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>
    public class CreditMusicFade : MonoBehaviour
    {
		[SerializeField] private float _musicFadeTime = 1f;
		private AudioSource _musicSource;

		private void Start()
		{
			_musicSource = GetComponent<AudioSource>();
			StopAllCoroutines();
			StartCoroutine(FadeInMusic());
		}

        private IEnumerator FadeInMusic()
		{
			float timeElapsed = 0f;

				while (timeElapsed < _musicFadeTime)
				{
					_musicSource.volume = Mathf.Lerp(0, 1, timeElapsed / _musicFadeTime);
					timeElapsed += Time.deltaTime;
					yield return null;
				}
		}
    }
}

using System.Collections;
using UnityEngine;

namespace MemoBubble
{
	/// <summary>
	/// Create after images for moving sprites.
	/// </summary>
	///
	/// <remarks>
	/// author: Jose Mäntylä
	/// </remarks>
	public class SpriteAfterImage : MonoBehaviour
	{
		[SerializeField] private float _invokeRepeatTime = 0.1f;
		[SerializeField] private float _afterImageDestroyTime = 0.5f;

		private void Start()
		{
			InvokeRepeating("SpawnAfterImage", 0, _invokeRepeatTime);
		}

		/// <summary>
		/// Spawns an after image behind the object at specified intervals.
		/// </summary>
		private void SpawnAfterImage()
		{
			GameObject afterImage = new GameObject();
			SpriteRenderer afterImageRenderer = afterImage.AddComponent<SpriteRenderer>();
			afterImageRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
			afterImage.transform.position = transform.position;
			afterImage.transform.localScale = transform.localScale;
			afterImageRenderer.sortingLayerName = "Particles";
			
			StartCoroutine(FadeAfterImage(afterImageRenderer));
			Destroy(afterImage, _afterImageDestroyTime);
		}

		/// <summary>
		/// Coroutine to make the after image fade away.
		/// </summary>
		/// <param name="afterImageRenderer">After image sprite renderer.</param>
		/// <returns></returns>
		IEnumerator FadeAfterImage(SpriteRenderer afterImageRenderer)
		{
			Color color = afterImageRenderer.color;
			color.a -= 0.2f;
			afterImageRenderer.color = color;

			yield return new WaitForEndOfFrame();
		}
	}
}

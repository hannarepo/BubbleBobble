using System.Collections;
using UnityEngine;

namespace BubbleBobble
{
	public class SpriteAfterImage : MonoBehaviour
	{
		private void Start()
		{
			InvokeRepeating("SpawnAfterImage", 0, 0.2f);
		}

		private void SpawnAfterImage()
		{
			GameObject afterImage = new GameObject();
			SpriteRenderer afterImageRenderer = afterImage.AddComponent<SpriteRenderer>();
			afterImageRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
			afterImage.transform.position = transform.position;
			afterImage.transform.localScale = transform.localScale;
			
			StartCoroutine(FadeAfterImage(afterImageRenderer));
			Destroy(afterImage, 0.5f);
		}

		IEnumerator FadeAfterImage(SpriteRenderer afterImageRenderer)
		{
			Color color = afterImageRenderer.color;
			color.a -= 0.5f;
			afterImageRenderer.color = color;

			yield return new WaitForEndOfFrame();
		}
	}
}

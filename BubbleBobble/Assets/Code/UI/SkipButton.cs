using UnityEngine;

namespace BubbleBobble
{
	public class SkipButton : MonoBehaviour
	{
		public void SetButtonActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}
	}
}

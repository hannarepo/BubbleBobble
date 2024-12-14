using UnityEngine;

namespace MemoBubble
{
	public class SkipButton : MonoBehaviour
	{
		public void SetButtonActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}
	}
}

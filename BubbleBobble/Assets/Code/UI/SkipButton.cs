using UnityEngine;

namespace BubbleBobble
{
	public class SkipButton : MonoBehaviour
	{
		[SerializeField] private LevelChanger _levelChanger;
		private InputReader _inputReader;

		private void Awake()
		{
			_inputReader = GetComponent<InputReader>();
		}

		private void Update()
		{
			if (_inputReader.Skip)
			{
				_levelChanger.SkipIntro();
			}
		}

		public void SetButtonActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}
	}
}

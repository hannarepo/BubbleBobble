using TMPro;
using UnityEngine;

namespace BubbleBobble
{
	public class CurrentLevelText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _levelNumberText;
		[SerializeField] private TMP_Text _worldNumberText;
		private int _levelCounter = 1;

		public void UpdateLevelNumber()
		{
			_levelCounter++;
			_levelNumberText.text = $"{_levelCounter}";
		}

		public void UpdateWorldNumber(int worldIndex)
		{
			_levelCounter = -1;
			_worldNumberText.text = $"{worldIndex}";
		}
	}
}

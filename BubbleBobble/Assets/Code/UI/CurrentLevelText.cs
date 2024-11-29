using TMPro;
using UnityEngine;

namespace BubbleBobble
{
	public class CurrentLevelText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _levelNumberText;
		[SerializeField] private TMP_Text _worldNumberText;

		public void UpdateLevelNumber(int levelIndex)
		{
			_levelNumberText.text = $"{levelIndex}";
		}

		public void UpdateWorldNumber(int worldIndex)
		{
			_worldNumberText.text = $"{worldIndex}";
		}
	}
}

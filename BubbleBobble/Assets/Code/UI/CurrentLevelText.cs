using TMPro;
using UnityEngine;

namespace BubbleBobble
{
	public class CurrentLevelText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _levelNumberText;
		[SerializeField] private TMP_Text _worldNumberText;
		[SerializeField] private int _transition1Number = 12;
		[SerializeField] private int _transition2Number = 23;
		private int _levelNumber = 1;
		private int _worldNumber = 1;
		private bool _skippedLevels = false;

		public bool SkippedLevels { set { _skippedLevels = value; } }

		public void UpdateLevelNumber()
		{
			if (_skippedLevels)
			{
				_levelNumber += 2;
				_skippedLevels = false;
			}
			else
			{
				_levelNumber++;
			}
			_levelNumberText.text = $"{_levelNumber}";
		}

		public void UpdateWorldNumber()
		{
			_worldNumber++;
			_worldNumberText.text = $"{_worldNumber}";
			_levelNumber = -1;
			UpdateLevelNumber();
		}
	}
}

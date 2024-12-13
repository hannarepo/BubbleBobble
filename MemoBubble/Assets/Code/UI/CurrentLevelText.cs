using TMPro;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Updates current level and world texts.
	/// </summary>
	public class CurrentLevelText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _levelNumberText;
		[SerializeField] private TMP_Text _worldNumberText;
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

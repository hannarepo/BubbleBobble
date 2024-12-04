using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BubbleBobble
{

	/// <summary>
	/// Handles the score text in the game.
	/// </summary>
	///
	/// <remarks>
	/// author: Juho Kokkonen
	/// </remarks>
	public class ScoreText : MonoBehaviour
	{
		public TextMeshProUGUI ScoreCounter;

		public void IncrementScoreCount(int scoreTotal)
		{
			ScoreCounter.text = $"{scoreTotal}";
		}

		public void DecrementScoreCount(int scoreTotal)
		{
			ScoreCounter.text = $"{scoreTotal}";
		}
	}
}
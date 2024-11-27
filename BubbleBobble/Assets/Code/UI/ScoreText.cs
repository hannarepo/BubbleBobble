using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BubbleBobble
{
    public class ScoreText : MonoBehaviour
    {
        public TextMeshProUGUI ScoreCounter;

        public void IncrementScoreCount(int scoreTotal)
        {
            ScoreCounter.text = $"{scoreTotal}";
        }
    }
}

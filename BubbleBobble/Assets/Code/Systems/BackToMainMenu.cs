using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleBobble
{
    public class BackToMainMenu : MonoBehaviour
    {
        [SerializeField] private float _backToMenuTime;
		private float _timer;

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_timer >= _backToMenuTime)
			{
				SceneManager.LoadScene("Main Menu");
			}
		}
    }
}

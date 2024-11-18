using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleBobble
{
    public class MainMenu : MonoBehaviour
    {
		[SerializeField] private string _sceneName;
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(_sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

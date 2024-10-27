using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleBobble
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync("DemoLevel1");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

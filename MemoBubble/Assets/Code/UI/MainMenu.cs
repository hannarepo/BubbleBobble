using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MemoBubble
{

    /// <summary>
    /// Handles the main menu.
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;


namespace BubbleBobble
{

    /// <summary>
    /// Handles the ingame UI and it's navigation with controller and keyboard.
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuOpen;
        [SerializeField] private GameObject _settingsMenuOpen;
        [SerializeField] private GameObject _HelpViewOpen;
        [SerializeField] private GameObject _exitOpen;



        [Header("Player scripts to disable on pause")]
        [SerializeField] private PlayerControl _playerControl;
        [SerializeField] private StoreInput _storeControl;

        [Header("Selected button on menus")]
        [SerializeField] private GameObject _pauseMenuFirstButton;
        [SerializeField] private GameObject _settingsMenuFirstButton;
        [SerializeField] private GameObject _helpViewFirstButton;
        [SerializeField] private GameObject _exitFirstButton;
        //[SerializeField] private GameObject _gameOverFirstButton;


        private bool isPaused;



        private void Start()
        {
            _pauseMenuOpen.SetActive(false);
            _settingsMenuOpen.SetActive(false);
            _HelpViewOpen.SetActive(false);
            _exitOpen.SetActive(false);
        }

        private void Update()
        {
            if (InputManagerUI.instance.MenuOpenCloseInput)
            {
                if (!isPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0;

            _playerControl.enabled = false;
            _storeControl.enabled = false;

            OpenPauseMenu();
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1;

            _playerControl.enabled = true;
            _storeControl.enabled = true;

            CloseAllMenus();
        }

        public void OpenPauseMenu()
        {
            _pauseMenuOpen.SetActive(true);
            _settingsMenuOpen.SetActive(false);
            _HelpViewOpen.SetActive(false);
            _exitOpen.SetActive(false);

            EventSystem.current.SetSelectedGameObject(_pauseMenuFirstButton);
        }

        public void OpenSettingsMenu()
        {
            _pauseMenuOpen.SetActive(true);
            _settingsMenuOpen.SetActive(true);
            _helpViewFirstButton.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_settingsMenuFirstButton);
        }

        public void OpenHelpView()
        {
            _pauseMenuOpen.SetActive(true);
            _settingsMenuOpen.SetActive(false);
            _HelpViewOpen.SetActive(true);



            EventSystem.current.SetSelectedGameObject(_helpViewFirstButton);
        }

        public void OpenExitConfirmation()
        {
            _pauseMenuOpen.SetActive(true);
            _settingsMenuOpen.SetActive(false);
            _HelpViewOpen.SetActive(false);
            _exitOpen.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_exitFirstButton);
        }

        private void CloseAllMenus()
        {
            _pauseMenuOpen.SetActive(false);
            _settingsMenuOpen.SetActive(false);
            _HelpViewOpen.SetActive(false);
            _exitOpen.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
        }
        public void Home()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
        }



        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }
}

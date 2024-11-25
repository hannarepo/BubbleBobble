using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


namespace BubbleBobble
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuGO;
        [SerializeField] private GameObject _settingsMenuGO;

        [Header("Player scripts to disable on pause")]
        [SerializeField] private PlayerControl _playerControl;

        [Header("Selcted button on menus")]
        [SerializeField] private GameObject _pauseMenuFirstButton;
        [SerializeField] private GameObject _settingsMenuFirstButton;


        private bool isPaused;



        private void Start()
        {
            _pauseMenuGO.SetActive(false);
            _settingsMenuGO.SetActive(false);
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

            OpenPauseMenu();
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1;

            _playerControl.enabled = true;

            CloseAllMenus();
        }

        private void OpenPauseMenu()
        {
            _pauseMenuGO.SetActive(true);
            _settingsMenuGO.SetActive(false);

            EventSystem.current.SetSelectedGameObject(_pauseMenuFirstButton);
        }

        public void OpenSettingsMenu()
        {
            _pauseMenuGO.SetActive(false);
            _settingsMenuGO.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_settingsMenuFirstButton);
        }

        private void CloseAllMenus()
        {
            _pauseMenuGO.SetActive(false);
            _settingsMenuGO.SetActive(false);

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

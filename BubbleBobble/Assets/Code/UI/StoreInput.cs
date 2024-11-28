using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace BubbleBobble
{

    /// <summary>
    /// Handles the store input and pausing the game.
    /// Also disables player control when paused.
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
    public class StoreInput : MonoBehaviour
    {
        [SerializeField] GameObject _storeOpen;

        [Header("Player scripts to disable on pause")]
        [SerializeField] private PlayerControl _playerControl;

        [Header("Selected button in store")]
        [SerializeField] private GameObject _storeFirstButton;

        private bool isPaused;

        private void Start()
        {
            _storeOpen.SetActive(false);
        }

        private void Update()
        {
            if (InputManagerUI.instance.StoreOpenCloseInput)
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

            OpenStore();
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1;

            _playerControl.enabled = true;

            _storeOpen.SetActive(false);
        }

        public void OpenStore()
        {
            _storeOpen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_storeFirstButton);
        }
    }
}

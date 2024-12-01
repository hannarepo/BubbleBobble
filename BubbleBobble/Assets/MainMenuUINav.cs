using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace BubbleBobble
{

    /// <summary>
    /// Handles the Main menu UI navigation with controller and keyboard buttons.
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
    public class MainMenuNav : MonoBehaviour
    {

        [SerializeField] GameObject _OptionsOpen;
		[SerializeField] GameObject _HelpOpen;
		[SerializeField] GameObject _ExitConfirmationOpen;


        [Header("Selected button in Options")]
		[SerializeField] private GameObject _MainMenuFirstButton;
        [SerializeField] private GameObject _OptionsFirstButton;
		[SerializeField] private GameObject _HelpFirstButton;
		[SerializeField] private GameObject _ExitFirstButton;



        private void Start()
        {
            _OptionsOpen.SetActive(false);
			_HelpOpen.SetActive(false);
			_ExitConfirmationOpen.SetActive(false);
			EventSystem.current.SetSelectedGameObject(_MainMenuFirstButton);
        }

        // private void Update()
        // {
        //     if (InputManagerUI.instance.StoreOpenCloseInput)
        //     {
        //         if (!isPaused)
        //         {
        //             OpenStore();
        //         }
        //         else
        //         {
        //             CloseStore();
        //         }
        //     }
        // }
        public void OpenOptions()
        {
            //Time.timeScale = 0;
            _OptionsOpen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_OptionsFirstButton);
        }

        public void CloseOptions()
        {
            //Time.timeScale = 1;
			_OptionsOpen.SetActive(false);
			EventSystem.current.SetSelectedGameObject(_MainMenuFirstButton);
        }

		public void OpenHelp()
		{
			_OptionsOpen.SetActive(false);
			_HelpOpen.SetActive(true);
			EventSystem.current.SetSelectedGameObject(_HelpFirstButton);
		}

		public void CloseHelp()
		{
			_OptionsOpen.SetActive(false);
			_HelpOpen.SetActive(false);
			EventSystem.current.SetSelectedGameObject(_MainMenuFirstButton);
		}

		public void OpenExitConfirmation()
		{
			_OptionsOpen.SetActive(false);
			_ExitConfirmationOpen.SetActive(true);
			EventSystem.current.SetSelectedGameObject(_ExitFirstButton);
		}

		public void CloseExitConfirmation()
		{
			_OptionsOpen.SetActive(false);
			_ExitConfirmationOpen.SetActive(false);
			EventSystem.current.SetSelectedGameObject(_MainMenuFirstButton);
		}
    }
}

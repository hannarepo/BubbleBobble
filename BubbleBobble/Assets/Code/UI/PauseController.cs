using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Handles the pause menu.
	/// Old Script, no longer in use AFAIK.
	/// </summary>
	///
	/// <remarks>
	/// author: Juho Kokkonen
	/// </remarks>
	public class PauseController : MonoBehaviour
	{

		[SerializeField] GameObject pauseMenu;
		private InputReader _inputReader;


		void Start()
		{
			_inputReader = GetComponent<InputReader>();
		}

		void Update()
		{
			if (_inputReader.PauseEsc)
			{
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
			}
		}
	}
}

using UnityEngine;

namespace BubbleBobble
{
	public class TransitionScreen : MonoBehaviour
	{
		[SerializeField] private float _speed = 1f;
		[SerializeField] private float _endYPosition = 1f;
		[SerializeField] private MainMenu _mainMenu;
		[SerializeField] private PauseMenu _pauseMenu;
		private AudioSource _audioSource;
		private bool _startMovement = false;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}


		private void Update()
		{
			if (_startMovement)
			{
				transform.position += Vector3.up * _speed * Time.deltaTime;
				
				if (transform.position.y > _endYPosition)
				{
					ChangeScene();
					_startMovement = false;
				}
			}
		}

		private void ChangeScene()
		{
			print("Changing scene");
			if (_mainMenu != null)
			{
				_mainMenu.PlayGame();
			}
			else if (_pauseMenu != null)
			{
				_pauseMenu.Home();
			}
		}

		public void StartSceneTransition()
		{
			_audioSource.Play();
			_startMovement = true;
		}
	}
}

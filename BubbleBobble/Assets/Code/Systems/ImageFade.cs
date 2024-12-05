using UnityEngine.UI;
using UnityEngine;

namespace BubbleBobble
{
	public class ImageFade : MonoBehaviour
	{
		public enum State
		{
			None = 0,
			FadeIn,
			FadeOut
		}

		[SerializeField] private float _fadeSpeed = 1f;
		[SerializeField] private Image _image;
		[SerializeField] private PauseMenu _pauseMenu;
		[SerializeField] private MainMenu _mainMenu;
		private AudioSource _audioSource;
		private State _state = State.None;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void Update()
		{
			switch (_state)
			{
				case State.FadeIn:
				FadeIn();
				if (Mathf.Approximately(_image.color.a, 1))
				{
					if (_pauseMenu != null)
					{
						_pauseMenu.Home();
					}
					else if (_mainMenu != null)
					{
						_mainMenu.PlayGame();
					}

					_state = State.None;
				}
				break;

				case State.FadeOut:
				FadeOut();
				if (Mathf.Approximately(_image.color.a, 0))
				{
					_state = State.None;
				}
				break;
			}
		}

		private void FadeIn()
		{
			float alpha = _image.color.a;
			alpha += Time.deltaTime * _fadeSpeed;
			SetAlpha(alpha);
			print("Fading in");
		}

		private void FadeOut()
		{
			float alpha = _image.color.a;
			alpha -= Time.deltaTime * _fadeSpeed;
			SetAlpha(alpha);
		}

		private void SetAlpha(float alpha)
		{
			Color color = _image.color;
			color.a = Mathf.Clamp01(alpha);
			_image.color = color;
		}

		public void StartFadeIn()
		{
			SetAlpha(0);
			_state = State.FadeIn;
			Time.timeScale = 1;
			if (_audioSource != null)
			{
				_audioSource.Play();
			}
		}
		public void StartFadeOut()
		{
			SetAlpha(1);
			_state = State.FadeOut;
		}
	}
}

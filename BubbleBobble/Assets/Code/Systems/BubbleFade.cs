using UnityEngine.UI;
using UnityEngine;

namespace BubbleBobble
{
	public class BubbleFade : MonoBehaviour
	{
		public enum State
		{
			None = 0,
			FadeIn,
			FadeOut
		}

		[SerializeField] private float _fadeSpeed = 1f;
		[SerializeField] private MainMenu _mainMenu;
		[SerializeField] private bool _fadeOutAtStart = false;
		private State _state = State.None;
		private Image _image;

		private void Awake()
		{
			_image = GetComponent<Image>();

		}
		private void Start()
		{
			_state = _fadeOutAtStart 
			? _state = State.FadeOut
			: _state = State.None;
		}

		private void Update()
		{
			switch (_state)
			{
				case State.FadeIn:
				FadeIn();
				if (Mathf.Approximately(_image.color.a, 1))
				{
					_state = State.None;
					if (_mainMenu != null)
					{
						_mainMenu.PlayGame();
					}
					
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
			_state = State.FadeIn;
		}
	}
}

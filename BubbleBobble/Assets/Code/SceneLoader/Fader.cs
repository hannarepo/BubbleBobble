using UnityEngine;
using UnityEngine.UI;

namespace SceneLoader
{
	public class Fader : MonoBehaviour
	{
		public enum State
		{
			None = 0,
			FadingIn,
			FadingOut
		}

		public static event System.Action FadeComplete;

		[SerializeField, Tooltip("Fade speed")]
		private float _speed = 1;

		[SerializeField, Tooltip("Image to fade")]
		private Image _image = null;

		// Faderin tämänhetkinen tila (FadingIn, FadingOut tai None)
		private State _state = State.None;

		public bool IsFading => _state != State.None;

		private void Awake()
		{
			// Haetaan viittaus Image-komponenttiin vain, jos sitä 
			// ei ole asetettu Unitystä.
			if (_image == null)
			{
				_image = GetComponent<Image>();
			}

			// Asetetaan alpha-arvo nollaksi, jotta kuva on aluksi näkymätön.
			SetAlpha(0);
		}

		private void Update()
		{
			switch (_state)
			{
				case State.FadingIn:
					FadeIn();

					if (Mathf.Approximately(_image.color.a, 1))
					{
						FadingDone();
					}
					break;

				case State.FadingOut:
					FadeOut();

					if (Mathf.Approximately(_image.color.a, 0))
					{
						FadingDone();
					}
					break;
			}
		}

		public bool StartFade(State state)
		{
			if (IsFading)
			{
				return false;
			}

			_state = state;
			return true;
		}

		private void FadingDone()
		{
			_state = State.None;

			// Jos FadeComplete-eventillä on kuuntelijoita, laukaistaan eventti.
			if (FadeComplete != null)
			{
				FadeComplete();
			}
		}

		private void FadeIn()
		{
			float alpha = _image.color.a;
			alpha += Time.deltaTime * _speed;
			SetAlpha(alpha);
		}

		private void FadeOut()
		{
			float alpha = _image.color.a;
			alpha -= Time.deltaTime * _speed;
			SetAlpha(alpha);
		}

		private void SetAlpha(float alpha)
		{
			// Haetaan viittaus väriin, jotta voidaan muuttaa alpha-arvoa.
			Color color = _image.color;
			// Varmistetaan, että alpha-arvo on välillä 0-1.
			color.a = Mathf.Clamp01(alpha);
			_image.color = color;
		}
	}
}

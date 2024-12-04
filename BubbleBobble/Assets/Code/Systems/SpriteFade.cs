using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace BubbleBobble
{
	public class SpriteFade : MonoBehaviour
	{
		public enum State
		{
			None = 0,
			FadeIn,
			FadeOut
		}

		public static event System.Action FadeComplete;

		[SerializeField] private float _fadeSpeed = 1f;
		[SerializeField] private SpriteRenderer _sprite;
		[SerializeField] private TMP_Text _text;
		[SerializeField] private bool _hasChildren = false;
		private State _state = State.None;
		private SpriteRenderer[] _childSprites = null;

		private void Awake()
		{
			if (_hasChildren)
			{
				_childSprites = GetComponentsInChildren<SpriteRenderer>();
			}
		}

		private void Update()
		{
			switch (_state)
			{
				case State.FadeIn:

				FadeIn();
				if (Mathf.Approximately(_sprite.color.a, 1))
				{
					FadingDone();
					_state = State.None;
				}
				break;

				case State.FadeOut:

				FadeOut();
				if (Mathf.Approximately(_sprite.color.a, 0))
				{
					FadingDone();
					_state = State.None;

				}
				break;
			}
		}

		private void FadeIn()
		{
			float alpha = _sprite.color.a;
			alpha += Time.deltaTime * _fadeSpeed;
			SetAlpha(alpha);
		}

		private void FadeOut()
		{
			float alpha = _sprite.color.a;
			alpha -= Time.deltaTime * _fadeSpeed;
			SetAlpha(alpha);
		}

		private void FadingDone()
		{
			if (FadeComplete != null)
			{
				FadeComplete();
			}
		}

		private void SetAlpha(float alpha)
		{
			Color color = _sprite.color;
			color.a = Mathf.Clamp01(alpha);

			if (_hasChildren)
			{
				foreach(SpriteRenderer child in _childSprites)
				{
					child.color = color;
				}
			}
			if (_text != null)
			{
				_text.color = color;
			}
			_sprite.color = color;
		}

		public void StartFadeIn()
		{
			_state = State.FadeIn;
		}
		public void StartFadeOut()
		{
			_state = State.FadeOut;
		}
	}
}

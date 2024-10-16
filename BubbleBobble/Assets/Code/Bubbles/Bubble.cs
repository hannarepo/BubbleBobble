using UnityEngine;

namespace BubbleBobble
{
	public abstract partial class Bubble : MonoBehaviour, IBubble
	{
		private bool _canPop = false;
		protected GameManager _gameManager;
		[SerializeField] private ParticleSystem _popEffectPrefab;
		private SpriteRenderer _spriteRenderer;
		private Collider2D _collider;

		protected abstract BubbleType Type
		{
			get;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			_gameManager = FindObjectOfType<GameManager>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_collider = GetComponent<Collider2D>();
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player") && _canPop)
			{
				PopBubble();
			}
		}

		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("PlayerFeet"))
			{
				CanPop(false);
			}
		}

		public virtual void PopBubble()
		{
			_spriteRenderer.enabled = false;
			_collider.enabled = false;

			float delay = 0;

			if (_popEffectPrefab != null)
			{
				ParticleSystem effect = Instantiate(_popEffectPrefab, transform.position, Quaternion.identity);
				delay = effect.main.duration + 0.2f;
				effect.Play(withChildren: true);
				Destroy(effect, delay);
			}

			_gameManager.BubblePopped(Type);
			Destroy(gameObject, delay);
		}

		public virtual void CanPop(bool canPop)
		{
			_canPop = canPop;
		}
		
	}
}

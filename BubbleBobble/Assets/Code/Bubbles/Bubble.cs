using TMPro;
using UnityEngine;

namespace BubbleBobble
{
/// <summary>
/// Abstract base class for the bubbles in the game.
/// </summary>
///
/// <remarks>
/// author: Jose Mäntylä, Hanna Repo
/// </remarks>

	public abstract class Bubble : MonoBehaviour, IBubble
	{
		[SerializeField] private BubbleData _bubbleData;
		[SerializeField] private float _moveSpeed = 1f;
		[SerializeField] private ParticleSystem _popEffectPrefab;
		[SerializeField] private AudioClip _popSFX;
		[SerializeField] private GameObject _pointEffectPrefab;
		private Audiomanager _audioManager;
		private bool _canPop = false;
		protected GameManager _gameManager;
		private SpriteRenderer _spriteRenderer;
		private Collider2D _collider;
		protected bool _canMoveBubble = false;
		private Rigidbody2D _rigidBody;
		private float _originalGravityScale;

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
			_rigidBody = GetComponent<Rigidbody2D>();
			_originalGravityScale = _rigidBody.gravityScale;
			_audioManager = FindObjectOfType<Audiomanager>();
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags._player) && _canPop)
			{
				PopBubble();
				// TODO: Add points
				_gameManager.HandleBubblePop(_bubbleData.Points);
			}
		}

		protected virtual void OnCollisionStay2D(Collision2D collision)
		{
			
		}

		protected virtual void OnCollisionExit2D(Collision2D collision)
		{
			
		}

		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag(Tags._playerFeet))
			{
				CanPop(false);
			}
			if (Type == BubbleType.Fire && collider.gameObject.CompareTag(Tags._platform)
				|| Type == BubbleType.Bomb && collider.gameObject.CompareTag(Tags._platform))
			{
				_rigidBody.gravityScale = 0;
				_rigidBody.velocity = Vector2.zero;
				_canMoveBubble = true;
			}
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
			if (Type == BubbleType.Fire && collider.gameObject.CompareTag(Tags._platform)
				|| Type == BubbleType.Bomb && collider.gameObject.CompareTag(Tags._platform))
			{
				_rigidBody.gravityScale = _originalGravityScale;
				_canMoveBubble = false;
				ChangeXDirection();
			}
		}

		/// <summary>
		/// Pop the bubble. Hide the bubble by disabling renderer and collider for immidiate feedback to player.
		/// Play pop effect and destroy the bubble and pop effect after the effect has finished.
		/// </summary>
		public virtual void PopBubble()
		{
			_spriteRenderer.enabled = false;
			_collider.enabled = false;

			float delay = 0;

			if (_audioManager != null)
			{
				_audioManager.PlaySFX(_popSFX);
			}

			if (_popEffectPrefab != null)
			{
				ParticleSystem effect = Instantiate(_popEffectPrefab, transform.position, Quaternion.identity);
				GameObject pointEffect = Instantiate(_pointEffectPrefab, transform.position, Quaternion.identity);
				pointEffect.GetComponentInChildren<TextMeshPro>().text = _bubbleData.Points.ToString();
				delay = Mathf.Max(delay, effect.main.duration + 0.5f);
				effect.Play(withChildren: true);
				Destroy(effect.gameObject, delay);
				Destroy(pointEffect, delay);
			}

			_gameManager.BubblePopped(Type);

			Destroy(gameObject);
		}

		public virtual void CanPop(bool canPop)
		{
			_canPop = canPop;
		}

		/// <summary>
		/// Moves the bubble on the X-axis.
		/// </summary>
		public virtual void BubbleMovement()
		{
			_rigidBody.AddForce(transform.right * _moveSpeed, ForceMode2D.Force);
		}

		/// <summary>
		/// Reverses the direction of the bubble movement on the X-axis.
		/// </summary>
		public virtual void ChangeXDirection()
		{
			_moveSpeed *= -1;
		}
	}
}

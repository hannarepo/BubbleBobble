using System;
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
		private bool _canPop = false;
		protected GameManager _gameManager;
		[SerializeField] private ParticleSystem _popEffectPrefab;
		private SpriteRenderer _spriteRenderer;
		private Collider2D _collider;
		protected bool _canMoveBubble = false;
		[SerializeField] private float _moveSpeed = 1f;
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
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player") && _canPop)
			{
				PopBubble();
			}
			if (Type == BubbleType.Fire && collision.gameObject.CompareTag("Platform")
			|| Type == BubbleType.Bomb && collision.gameObject.CompareTag("Platform"))
			{
				_rigidBody.gravityScale = 0;
				_canMoveBubble = true;
			}
		}

		/* protected virtual void OnCollisionStay2D(Collision2D collision)
		{
			if (Type == BubbleType.Fire && collision.gameObject.CompareTag("Platform")
			|| Type == BubbleType.Bomb && collision.gameObject.CompareTag("Platform"))
			{
				_canMoveBubble = true;
				print("Collision stay");
			}
		} */
		protected virtual void OnCollisionExit2D(Collision2D collision)
		{
			if (Type == BubbleType.Fire && collision.gameObject.CompareTag("Platform")
			|| Type == BubbleType.Bomb && collision.gameObject.CompareTag("Platform"))
			{
				_rigidBody.gravityScale = _originalGravityScale;
				_canMoveBubble = false;
				ChangeXDirection();
			}
		}

		/* protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("PlayerFeet"))
			{
				CanPop(false);
			}
			if (Type == BubbleType.Fire && collider.CompareTag("PlatformTilemap")
			|| Type == BubbleType.Bomb && collider.CompareTag("PlatformTilemap"))
			{
				print("Trigger enter");
				_rigidBody.gravityScale = 0;
				_canMoveBubble = true;
			}
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
			if (Type == BubbleType.Fire && collider.CompareTag("PlatformTilemap")
			|| Type == BubbleType.Bomb && collider.CompareTag("PlatformTilemap"))
			{
				print("Trigger exit");
				_rigidBody.gravityScale = _originalGravityScale;
				_canMoveBubble = false;
				ChangeXDirection();
			}
		} */

		/// <summary>
		/// Pop the bubble. Hide the bubble by disabling renderer and collider for immidiate feedback to player.
		/// Play pop effect and destroy the bubble and pop effect after the effect has finished.
		/// </summary>
		public virtual void PopBubble()
		{
			_spriteRenderer.enabled = false;
			_collider.enabled = false;

			float delay = 0;

			if (_popEffectPrefab != null)
			{
				ParticleSystem effect = Instantiate(_popEffectPrefab, transform.position, Quaternion.identity);
				delay = effect.main.duration + 0.5f;
				effect.Play(withChildren: true);
				Destroy(effect.gameObject, delay);
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
			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
			rb.AddForce(transform.right * _moveSpeed, ForceMode2D.Force);
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

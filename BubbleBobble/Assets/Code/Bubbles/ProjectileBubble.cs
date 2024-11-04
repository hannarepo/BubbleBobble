/// <remarks>
/// author: Hanna Repo
/// </remarks>
///
/// <summary>
/// After instantiating projectile bubble, check in which direction bubble
/// should be moving. Add force to bubble in that direction. Set timer for bubble lifetime.
/// After force is applied, set gravity scale to negative so that bubble floats up.
/// When bubble reaches the top of the level, destroy bubble after a short delay.
/// </summary>

using UnityEditor.Rendering;
using UnityEngine;

namespace BubbleBobble
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ProjectileBubble : Bubble
	{
		private Rigidbody2D _rb;
		[SerializeField] private float _floatingGravityScale;
		[SerializeField] private float _launchForce = 9f;
		[SerializeField] private float _launchForceWithBoost = 15f;
		[SerializeField] private float _trapWindow = 3f;
		[SerializeField] GameObject _trappedEnemyPrefab;
		[SerializeField] private float _lifeTime = 10f;
		[SerializeField] private Vector3 _sizeWithBoost = new Vector3(0.7f, 0.7f, 0.7f);
		private Vector3 _size;
		private float _timer = 0;

		protected override BubbleType Type
		{
			get { return BubbleType.Projectile; }
		}

		protected override void Awake()
		{
			CanPop(true);
		}

		protected override void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			base.Start();
		}

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_timer >= _lifeTime)
			{
				PopBubble();
			}

			// Projectile can trap enemies wihtin the given time window.
			// If timer is outside given trap window, collision between enemy and projectile is ignored.
			if (_timer >= _trapWindow)
			{
				Physics2D.IgnoreLayerCollision(12, 13, true);
				if (_size == _sizeWithBoost)
				{
					transform.localScale = _size * 1.2f;
				}
				else
				{
					transform.localScale = _size * 1.5f;
				}
				_rb.gravityScale = _floatingGravityScale;
			}
			else
			{
				Physics2D.IgnoreLayerCollision(12, 13, false);
			}
		}

		public void Launch(bool shootRight, bool forceBoostIsActive, bool sizeBoostIsActive)
		{
			if (sizeBoostIsActive)
			{
				_size = _sizeWithBoost;
			}
			else
			{
				_size = transform.localScale;
			}
			transform.localScale = _size;

			_rb = GetComponent<Rigidbody2D>();

			if (forceBoostIsActive)
			{
				_launchForce = _launchForceWithBoost;
			}

			// If player is facing right, bubble gets force applied to right from transform.
			// If player is facing left, bubble gets force applied to left from transform.
			if (shootRight)
			{
				_rb.AddForce(transform.right * _launchForce, ForceMode2D.Impulse);
			}
			else
			{
				_rb.AddForce(transform.right*-1 * _launchForce, ForceMode2D.Impulse);
			}
		}

		protected override void OnCollisionEnter2D(Collision2D collision)
		{
			base.OnCollisionEnter2D(collision);

			// If projectile bubble hits wall, set gravity scale to floating gravity scale.
			if (collision.gameObject.CompareTag("Wall"))
			{
				_rb.gravityScale = _floatingGravityScale;
			}

			// If projectile bubble hits enemy within given time window, trap the enemy.
			// If enemy is not invincible, instantiate trapped enemy bubble and destroy projectile bubble.
			if (collision.gameObject.CompareTag("Enemy") && _timer < _trapWindow)
			{
				if (!collision.gameObject.GetComponent<EnemyInvincibility>().IsInvincible)
				{
					GameObject enemy = collision.gameObject;
					GameObject trappedEnemy = Instantiate(_trappedEnemyPrefab, transform.position, Quaternion.identity);

					TrappedEnemyBubble trappedEnemyBubble = trappedEnemy.GetComponent<TrappedEnemyBubble>();

					if (trappedEnemyBubble != null)
					{
						trappedEnemyBubble.Enemy = enemy;
					}

					_gameManager.RemoveProjectileFromList(gameObject);
					Destroy(gameObject);
				}
			}
		}

		public override void PopBubble()
		{
			_gameManager.RemoveProjectileFromList(gameObject);

			base.PopBubble();
		}
	}
}

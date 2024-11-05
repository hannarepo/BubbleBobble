using UnityEngine;

namespace BubbleBobble
{
/// <summary>
/// After instantiating projectile bubble, check which direction bubble
/// should be moving. Add force to bubble in that direction. Set timer for bubble lifetime.
/// After force is applied, set gravity scale to negative so that bubble floats up.
/// If bubble does not trap enemy or is not popped by player, destroy bubble after a short delay.
/// </summary>
/// <remarks>
/// author: Hanna Repo
/// </remarks>
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

			// Pop bubble when it's lifetime runs out.
			if (_timer >= _lifeTime)
			{
				PopBubble();
			}

			// Projectile can trap enemies wihtin the given time window.
			// If timer is outside given trap window, collision between enemy and projectile is ignored.
			if (_timer >= _trapWindow)
			{
				gameObject.layer = LayerMask.NameToLayer("IgnoreEnemy");
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
				gameObject.layer = LayerMask.NameToLayer("ProjectileBubble");
			}
		}

		public void Launch(bool shootRight, bool forceBoostIsActive, bool sizeBoostIsActive)
		{
			// If bubble size power up is active, bubble size will be size with boost,
			// otherwise bubble size is as it's set in inspector.
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

			// Check whether force power up is active, if it is set launchforce accordigly.
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
			if (collision.gameObject.CompareTag(Tags._wall))
			{
				_rb.gravityScale = _floatingGravityScale;
			}

			// If projectile bubble hits enemy within given time window, trap the enemy.
			// If enemy is not invincible, instantiate trapped enemy bubble and destroy projectile bubble.
			if (collision.gameObject.CompareTag(Tags._enemy) && _timer < _trapWindow)
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
			// Remove projectile from list when popped.
			_gameManager.RemoveProjectileFromList(gameObject);

			base.PopBubble();
		}
	}
}

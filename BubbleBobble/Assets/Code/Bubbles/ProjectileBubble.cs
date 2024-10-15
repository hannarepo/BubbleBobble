/// <remarks>
/// author: Hanna Repo
/// </remarks>
///
/// <summary>
/// After instantiating projectile bubble, check in which direction bubble
/// should be moving. Move bubble in correct direction for range distance 
/// after which set gravity scale to negative so that bubble floats up.
/// When bubble reaches the top of the level, destroy bubble after a short delay.
/// </summary>

using UnityEngine;

namespace BubbleBobble
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ProjectileBubble : Bubble
	{
		private Rigidbody2D _rb;
		[SerializeField] private float _floatingGravityScale;
		[SerializeField] private float _launchForce = 5f;
		[SerializeField] private float _trapWindow = 3f;
		//[SerializeField] private PlayerControl _playerControl;
		private bool _shootRight;
		[SerializeField] GameObject _trappedEnemyPrefab;
		[SerializeField] private float _lifeTime = 10f;
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

			// Check which way player is facing from PlayerControl
			//_shootRight = FindObjectOfType<PlayerControl>().LookingRight;
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
				transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				_rb.gravityScale = _floatingGravityScale;
			}
			else
			{
				Physics2D.IgnoreLayerCollision(12, 13, false);
			}
		}

		public void Launch(bool shootRight)
		{
			_rb = GetComponent<Rigidbody2D>();

			// If player is facing right, bubble get force applied to right from transform.
			// If player is facing left, bubble get force applied to left from transform.
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

			if (collision.gameObject.CompareTag("Wall"))
			{
				_rb.gravityScale = _floatingGravityScale;
			}

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

					Destroy(gameObject);
				}
			}
		}

		
	}
}

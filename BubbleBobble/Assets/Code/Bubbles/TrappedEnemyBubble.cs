using UnityEngine;

namespace BubbleBobble
{
/// <summary>
/// When enemy is trapped in a bubble, it is not destroyed immediately. Instead, it is hidden and
/// the bubble floats up. After a certain time, the enemy is freed from the bubble and bubble is destroyed.
/// If player pops the bubble before the time limit, the enemy is destroyed.
/// </summary>
///
/// <remarks>
/// author: Hanna Repo
/// </remarks>

	public class TrappedEnemyBubble : Bubble
	{
		private Transform _transform;
		private Rigidbody2D _rb;
		private float _timer;
		[SerializeField] private float _timeLimit = 10f;
		[SerializeField] private float _floatingGravityScale = -0.5f;
		private GameObject _enemy;

		public GameObject Enemy
		{
			get { return _enemy; }
			set { _enemy = value; }
		}

		protected override void Awake()
		{
			_transform = transform;
			CanPop(true);
			_rb = GetComponent<Rigidbody2D>();
			_rb.gravityScale = _floatingGravityScale;
		}

		protected override void Start()
		{
			base.Start();
			_enemy.SetActive(false);
		}

		protected override BubbleType Type
		{
			get { return BubbleType.TrappedEnemy; }
		}

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_enemy == null)
			{
				Destroy(gameObject);
			}

			// If time limit is reached, free the enemy from the bubble and destroy the bubble.
			// Enemy's transform is set to bubble's transform so that enemy is freed at the same position where bubble is.
			// Enemy is set to invincible so that it can't be trapped immiadetly again.
			if (_timer >= _timeLimit)
			{
				_enemy.SetActive(true);
				_enemy.transform.position = _transform.position;
				_enemy.GetComponent<EnemyInvincibility>().FreeEnemy();
				Destroy(gameObject);
			}
		}

		protected override void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags.Player) && _canPop)
			{
				_enemy.SetActive(true);
				_enemy.transform.position = transform.position;
				_enemy.GetComponent<EnemyManagement>().LaunchAtDeath(true);
			}
			base.OnCollisionEnter2D(collision);
		}
	}
}

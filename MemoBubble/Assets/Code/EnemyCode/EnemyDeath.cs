using UnityEngine;

namespace MemoBubble
{
	/// <summary>
	/// Handles the death of an enemy. It will launch the enemy in a random direction
	/// and spawn an item after a certain amount of time.
	/// </summary>
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private Item[] _itemPrefabs;
		[SerializeField] private float _deathDelay = 4f;
		[SerializeField] private float _launchForce = 5f;
		[SerializeField] private Color _deathColor;
		[SerializeField] private Color _hurryUpColor;
		[SerializeField] private float _rotationSpeed = 500f;
		[SerializeField] private float _topTriggerYPos;
		[SerializeField] private float _bottomTriggerYPos;
		[SerializeField] private AudioClip _launchSFX;
		private GameManager _gameManager;
		private Audiomanager _audioManager;
		private LevelManager _levelManager;
		private Transform _levelParent;
		private Rigidbody2D _rb;
		private SpriteRenderer _spriteRenderer;
		private bool _launched = false;
		private bool _canSpawn = false;
		private float _timer = 0f;
		private Animator _animator;
		private BasicEnemyBehavior _movement;
		private BouncingEnemyMovement _bounce;


		void Start()
		{
			// Find the Game Manager and add this enemy object to the list of enemies
			_gameManager = FindObjectOfType<GameManager>();
			_gameManager.AddEnemyToList(gameObject);

			_rb = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_audioManager = FindObjectOfType<Audiomanager>();
			_levelParent = FindObjectOfType<LevelManager>().transform;
			_levelManager = FindObjectOfType<LevelManager>();
			_animator = GetComponent<Animator>();
			_movement = GetComponent<BasicEnemyBehavior>();
			_bounce = GetComponent<BouncingEnemyMovement>();

		}

		private void Update()
		{
			// Check if the enemy is out of level bounds and move it to the other side
			if (transform.position.y < _bottomTriggerYPos)
			{
				transform.position = new Vector3(transform.position.x, _topTriggerYPos, 0);
			}
			else if (transform.position.y > _topTriggerYPos)
			{
				transform.position = new Vector3(transform.position.x, _bottomTriggerYPos, 0);
			}

			// When enemy is launched, rotate it and spawn an item after a certain amount of time
			if (_launched)
			{
				transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
				_timer += Time.deltaTime;
				if (_timer > _deathDelay)
				{
					gameObject.layer = LayerMask.NameToLayer("Item");
					_canSpawn = true;
				}
			}

			// Change color of enemy when hurry up is active or when enemy is launched
			if (_levelManager.IsHurryUpActive && !_launched)
			{
				_spriteRenderer.color = _hurryUpColor;
			}
			else if (_launched)
			{
				_spriteRenderer.color = _deathColor;
			}
			else
			{
				_spriteRenderer.color = Color.white;
			}
		}

		public void SpawnItem()
		{
			_launched = false;
			int randomItem = Random.Range(0, _itemPrefabs.Length);
			Instantiate(_itemPrefabs[randomItem], transform.position, Quaternion.identity, _levelParent);
			Destroy(gameObject);
		}

		/// <summary>
		/// When enemy is killed, it is launched either to up and right or up and left.
		/// All enemy other enemy scripts are disabled and layer and tag is set to DeadEnemy so that
		/// player won't take damage from a dead enemy and launched enemy won't collide with platforms.
		/// </summary>
		/// <param name="playSFX">True if death sound effect will be played.</param>
		public void LaunchAtDeath(bool playSFX)
		{
			if (playSFX)
			{
				_audioManager.PlaySFX(_launchSFX);
			}

			gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
			gameObject.tag = Tags.DeadEnemy;
			_launched = true;
			_rb.constraints = RigidbodyConstraints2D.None;

			if (_movement != null)
			{
				_movement.enabled = false;
			}
			if (_bounce != null)
			{
				_bounce.enabled = false;
			}
			if (_animator != null)
			{
				_animator.enabled = false;
			}

			int randomInt = Random.Range(0, 2);
			Vector2 launchDirection = new Vector2(0, 0);
			if (randomInt == 0)
			{
				launchDirection = new Vector2(-1, 1);
			}
			else if (randomInt == 1)
			{
				launchDirection = new Vector2(1, 1);
			}

			_rb.AddForce(launchDirection * _launchForce, ForceMode2D.Impulse);
			_gameManager.RemoveEnemyFromList(gameObject);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (_launched && _canSpawn)
			{
				if (other.gameObject.CompareTag(Tags.Ground) || other.gameObject.CompareTag(Tags.Platform))
				{
					SpawnItem();
				}
			}
		}
	}
}

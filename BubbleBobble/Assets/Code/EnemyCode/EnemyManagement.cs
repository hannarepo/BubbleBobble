using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering;

namespace BubbleBobble
{
	public class EnemyManagement : MonoBehaviour
	{
		[SerializeField] private Item[] _itemPrefabs;
		[SerializeField] private float _deathDelay = 4f;
		[SerializeField] private float _launchForce = 5f;
		[SerializeField] private Color _deathColor;
		[SerializeField] private float _rotationSpeed = 500f;
		[SerializeField] private float _triggerYPos;
		[SerializeField] private float _ySpawnPos;
		[SerializeField] private AudioClip _launchSFX;
		private GameManager _gameManager;
		private Audiomanager _audioManager;
		private LevelChanger _levelChanger;
		private Transform _levelParent;
		private Rigidbody2D _rb;
		private SpriteRenderer _spriteRenderer;
		private bool _launched = false;
		private bool _canSpawn = false;
		private float _timer = 0f;
		private Animator _animator;
		private BasicEnemyBehavior _movement;
		private BouncingEnemyMovement _bounce;


		// Find the Game Manager and add this enemy object to the list of enemies
		void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_gameManager = FindObjectOfType<GameManager>();
			_audioManager = FindObjectOfType<Audiomanager>();
			_gameManager.AddEnemyToList(gameObject);
			_levelParent = FindObjectOfType<LevelManager>().transform;
			_levelChanger = FindObjectOfType<LevelChanger>();
			_animator = GetComponent<Animator>();
			_movement = GetComponent<BasicEnemyBehavior>();
			_bounce = GetComponent<BouncingEnemyMovement>();
		}

		private void Update()
		{
			if (transform.position.y < _triggerYPos && _levelChanger.IsLevelLoaded)
			{
				transform.position = new Vector3(transform.position.x, _ySpawnPos, 0);
			}

			if (_launched)
			{
				transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
				_timer += Time.deltaTime;
				if (_timer > _deathDelay)
				{
					gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
					_canSpawn = true;
				}
			}
		}

		public void SpawnItem()
		{
			_launched = false;
			int randomItem = Random.Range(0, _itemPrefabs.Length);
			Instantiate(_itemPrefabs[randomItem], transform.position, Quaternion.identity, _levelParent);
			_gameManager.RemoveEnemyFromList(gameObject);
		}

		public void LaunchAtDeath()
		{
			_audioManager.PlaySFX(_launchSFX);
			gameObject.layer = LayerMask.NameToLayer("IgnorePlatform");
			gameObject.tag = Tags._deadEnemy;
			_spriteRenderer.color = _deathColor;
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
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (_launched && _canSpawn)
			{
				if (other.gameObject.CompareTag(Tags._ground) || other.gameObject.CompareTag(Tags._platform))
				{
					SpawnItem();
				}
			}
		}
	}
}

/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Used to spawn special bubbles.
/// </summary>
using UnityEngine;

namespace BubbleBobble
{
	public class BubbleSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _fireBubblePrefab;
		[SerializeField] private GameObject _bombBubblePrefab;
		private int _fireBubblesSpawned = 0;
		[SerializeField] private bool _spawnFromTop = false;
		[SerializeField] private bool _moveLeft = false;
		private LevelChanger _levelChanger;
		[SerializeField] private Transform _secondarySpawnPoint;
		[SerializeField] private float _spawnRate = 5f;
		[SerializeField] private float _spawnLimit = 5f;
		[SerializeField] private GameManager _gameManager;
		private float _timeToSpawn = 0f;
		private bool _spawnSwitch = false;
		[SerializeField] private bool _alternateSpawns = false;
		[SerializeField] private Collider2D _topCollider;
		[SerializeField] private Collider2D _bottomCollider;
		public bool SpawnFromTop
		{
			get { return _spawnFromTop; }
		}

		#region Unity Functions

		private void Awake()
		{
			_levelChanger = FindObjectOfType<LevelChanger>();
			_gameManager = FindObjectOfType<GameManager>();
			_secondarySpawnPoint = transform.Find("SecondarySpawnPoint");
		}

		private void Start()
		{
			_gameManager.BubbleSpawnerInitialization();
			_topCollider.enabled = !_spawnFromTop;
			_bottomCollider.enabled = _spawnFromTop;
		}
		private void Update()
		{
			_timeToSpawn += Time.deltaTime;
			if (_timeToSpawn >= _spawnRate && _levelChanger.IsLevelLoaded)
			{
				SpawnSpecialBubble();
			}
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject.CompareTag(Tags._bubble))
			{
				if (collider.gameObject.transform.position.x < 0 && _alternateSpawns)
				{
					collider.gameObject.transform.position = _secondarySpawnPoint.position;
				}
				else
				{
					collider.gameObject.transform.position = gameObject.transform.position;
				}
			}
		}

		#endregion

		#region Spawners
		private void SpawnSpecialBubble()
		{
			if (_fireBubblesSpawned > _spawnLimit)
			{
				return;
			}
			// To be reworked
			SpawnFireBubble();
			_timeToSpawn = 0f;
		}
		public void SpawnBomb()
		{
			GameObject bombBubble = Instantiate(_bombBubblePrefab, gameObject.transform.position, Quaternion.identity);
			FloatDirection(bombBubble);
			bombBubble.GetComponent<BombBubble>().MoveLeft = _moveLeft;
		}

		/// <summary>
		/// Instantiates a fire bubble, checks which direction it should float.
		/// </summary>
		private void SpawnFireBubble()
		{
			GameObject fireBubble;
			if (_spawnSwitch)
			{
				fireBubble = Instantiate(_fireBubblePrefab, _secondarySpawnPoint.position, Quaternion.identity);
				_spawnSwitch = false;
			}
			else
			{
				fireBubble = Instantiate(_fireBubblePrefab, gameObject.transform.position, Quaternion.identity);
				_spawnSwitch = true;
			}
			//Instantiate(_fireBubblePrefab, gameObject.transform, worldPositionStays: false);
			FloatDirection(fireBubble);
			fireBubble.GetComponent<FireBubble>().MoveLeft = _moveLeft;
			_fireBubblesSpawned++;
			if (_alternateSpawns)
			{
				_moveLeft = !_moveLeft;
			}
		}

		#endregion

		private GameObject FloatDirection(GameObject bubble)
		{
			Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
			if (_spawnFromTop && rb.gravityScale < 0)
			{
				rb.gravityScale *= -1;
			}
			else if (!_spawnFromTop && rb.gravityScale > 0)
			{
				rb.gravityScale *= -1;
			}
			return bubble;
		}
	}
}
// TODO: Add the bubble teleport to bubble spawner 
// to teleport bubbles to the right spawnpoints

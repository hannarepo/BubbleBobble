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

		public bool SpawnFromTop
		{
			get { return _spawnFromTop; }
		}
		[SerializeField] private float _spawnRate = 5f;
		[SerializeField] private float _spawnLimit = 5f;
		[SerializeField] private GameManager _gameManager;
		private float _timeToSpawn = 0f;

		#region Unity Functions

		private void Awake()
		{
			_levelChanger = FindObjectOfType<LevelChanger>();
			_gameManager = FindObjectOfType<GameManager>();
		}

		private void Start()
		{
			_gameManager.BubbleSpawnerInitialization();
		}
		private void Update()
		{
			_timeToSpawn += Time.deltaTime;
			if (_timeToSpawn >= _spawnRate && _levelChanger.IsLevelLoaded)
			{
				SpawnSpecialBubble();
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
			_fireBubblesSpawned++;
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
			GameObject fireBubble = Instantiate(_fireBubblePrefab, gameObject.transform, worldPositionStays: false);
			FloatDirection(fireBubble);
			fireBubble.GetComponent<FireBubble>().MoveLeft = _moveLeft;
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

/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Used to spawn special bubbles.
/// </summary>
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BubbleBobble
{
	public class BubbleSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _specialBubblePrefab;
		[SerializeField] private GameObject _bombBubblePrefab;
		[SerializeField] private bool _spawnFromTop = false;
		[SerializeField] private bool _moveLeft = false;
		[SerializeField] private Transform _secondarySpawnPoint;
		[SerializeField] private float _spawnRate = 5f;
		[SerializeField] private float _spawnLimit = 5f;
		[SerializeField] private GameManager _gameManager;
		[SerializeField] private bool _alternateSpawns = false;
		[SerializeField] private Collider2D _topCollider;
		[SerializeField] private Collider2D _bottomCollider;
		private LevelChanger _levelChanger;
		private float _timeToSpawn = 0f;
		private bool _spawnSwitch = false;
		private int _specialBubblesSpawned = 0;
		public bool SpawnFromTop
		{
			get { return _spawnFromTop; }
		}

		#region Unity Functions

		private void Awake()
		{
			_secondarySpawnPoint = transform.Find("SecondarySpawnPoint");
		}

		private void Start()
		{
			_gameManager = FindObjectOfType<GameManager>();
			_gameManager.BubbleSpawnerInitialization(this);
			_levelChanger = _gameManager.GetComponent<LevelChanger>();

			if (_spawnFromTop)
			{
				_topCollider.enabled = false;
			}
			else
			{
				_bottomCollider.enabled = false;
			}
			//_topCollider.enabled = !_spawnFromTop;
			//_bottomCollider.enabled = _spawnFromTop;
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
					collider.gameObject.transform.position = transform.position;
				}
			}
		}

		#endregion

		#region Spawners

		/// <summary>
		/// Instantiates a special bubble and adjusts its' movement
		/// </summary>
		private void SpawnSpecialBubble()
		{
			if (_specialBubblesSpawned > _spawnLimit)
			{
				return;
			}

			GameObject specialBubble;
			if (_spawnSwitch)
			{
				specialBubble = Instantiate(_specialBubblePrefab, _secondarySpawnPoint.position, Quaternion.identity, _secondarySpawnPoint);
				_spawnSwitch = false;
			}
			else
			{
				specialBubble = Instantiate(_specialBubblePrefab, transform.position, Quaternion.identity, transform);
				_spawnSwitch = true;
			}
			FloatDirection(specialBubble);
			MoveDirection(specialBubble);
			_specialBubblesSpawned++;
			if (_alternateSpawns)
			{
				_moveLeft = !_moveLeft;
			}

			_timeToSpawn = 0f;
		}
		public void SpawnBomb()
		{
			GameObject bombBubble = Instantiate(_bombBubblePrefab, transform.position, Quaternion.identity, transform);
			FloatDirection(bombBubble);
			bombBubble.GetComponent<BombBubble>().MoveLeft = _moveLeft;
		}

		#endregion Spawners

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

		private void MoveDirection(GameObject bubble)
		{
			if (bubble.TryGetComponent<FireBubble>(out FireBubble fBubble))
			{
				fBubble.MoveLeft = _moveLeft;
			}
			else if (bubble.TryGetComponent<GlitchBubble>(out GlitchBubble gBubble))
			{
				gBubble.MoveLeft = _moveLeft;
			}
		}
	}
}

using UnityEngine;
using TMPro;

namespace BubbleBobble
{
		public class Health : MonoBehaviour
		{
		[SerializeField] private int _maxLives = 6;
		[SerializeField] private int _startLives = 4;
		[SerializeField] private Transform _playerReturnPoint;
		[SerializeField] private TMP_Text _gameOverText;
		[SerializeField] private float _invincibilityTime = 1f;
		[SerializeField] private float _flashRate = 1 / 10f;
		[SerializeField] private Vector3[] _heartPositions;
		[SerializeField] private GameObject _heartPrefab;
		[SerializeField] private GameObject _brokenHeartPrefab;
		private GameObject[] _hearts;
		private GameObject[] _brokenHearts;
		private int _currentLives;
		private Transform _transform;
		private float _invincibilityTimer = 0;
		private SpriteRenderer _spriteRenderer;
		private float _flashTimer = 0;
		private Rigidbody2D _rb;
		private InputReader _inputReader;

		public bool IsInvincible
		{
			get { return _invincibilityTimer > 0 ; }
		}

		public int CurrentLives => _currentLives;

		public int MaxLives => _maxLives;

		private void Awake()
		{
			_transform = transform;
			_currentLives = _maxLives;
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_inputReader = GetComponent<InputReader>();
			_rb = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			_currentLives = _startLives;
			_hearts = new GameObject[_maxLives];
			_brokenHearts = new GameObject[_maxLives];
			
			for (int i = 0; i < _startLives; i++)
			{
				_hearts[i] = Instantiate(_heartPrefab, _heartPositions[i], Quaternion.identity);
			}
		}

		private void Update()
		{
			print(_currentLives);
			if (IsInvincible)
			{
				_invincibilityTimer -= Time.deltaTime;
				_flashTimer += Time.deltaTime;
				_inputReader.enabled = false;
				_rb.constraints = RigidbodyConstraints2D.FreezeAll;

				if (_flashTimer > _flashRate)
				{
					Flash();
				}
			}

			if (!IsInvincible)
			{
				_spriteRenderer.enabled = true;
				_flashTimer = 0;
				_inputReader.enabled = true;
				_rb.constraints = RigidbodyConstraints2D.None;
				_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			}

			if (_currentLives == 0)
			{
				_gameOverText.gameObject.SetActive(true);
				Flash();
				Die();
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Enemy") && !IsInvincible && _currentLives > 0)
			{
				Invoke("Respawn", 1f);
				_currentLives--;
				Destroy(_hearts[_currentLives]);
				_brokenHearts[_currentLives] = Instantiate(_brokenHeartPrefab, _heartPositions[_currentLives], Quaternion.identity);

				if (_currentLives > 0)
				{
					_invincibilityTimer = _invincibilityTime;
				}
			}
		}

		public void SetExtraLife(bool set)
		{
			if (set)
			{
				Destroy(_brokenHearts[_currentLives]);
				_hearts[_currentLives] = Instantiate(_heartPrefab, _heartPositions[_currentLives], Quaternion.identity);
				_currentLives++;
			}
		}

		private void Respawn()
		{
			_transform.position = _playerReturnPoint.position;
		}

		private void Flash()
		{
			_spriteRenderer.enabled = !_spriteRenderer.enabled;
			_flashTimer = 0;
		}

		private void Die()
		{
			Destroy(gameObject, 2f);
		}
	}
}

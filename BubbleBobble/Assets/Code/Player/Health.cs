using UnityEngine;
using TMPro;

namespace BubbleBobble
{
	/// <summary>
	/// Class for managing player health. Player has set number of
	/// lives at the start of the game. Max lives and start lives are
	/// set in editor. When player loses a life, they are respawn at
	/// set location and flash for a given time. Hearts indicate current lives
	/// and when losing a life, hearts change to broken hearts. When player
	/// loses all lives the game is over.
	/// </summary>
	/// 
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

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
			
			for (int i = 0; i < _maxLives; i++)
			{
				_hearts[i] = Instantiate(_heartPrefab, _heartPositions[i], Quaternion.identity);
				 if (i >= _startLives)
				 {
					_hearts[i].SetActive(false);
				 }
			}
		}

		private void Update()
		{
			// Player has a short invincibility time after losing a life
			// so they won't lose one immidiately after.
			// If player is invincible, freeze rigidbody so player can't
			// move while respawning and do flashing method.
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
			// If player hits an enemy, they lose a life and are respawned to set location.
			// A heart is disabled and a broken heart is instatiated in it's place to
			// indicate loss of life to player.
			if (collision.gameObject.CompareTag(Tags._enemy) && !IsInvincible && _currentLives > 0)
			{
				Invoke("Respawn", 1f);
				_currentLives--;
				_hearts[_currentLives].SetActive(false);
				_brokenHearts[_currentLives] = Instantiate(_brokenHeartPrefab, _heartPositions[_currentLives], Quaternion.identity);

				if (_currentLives > 0)
				{
					_invincibilityTimer = _invincibilityTime;
				}
			}
		}

		public void SetExtraLife(bool value)
		{
			// If player has bought an extra life from the shop, a heart
			// is set active. If player has lost a life before bying the extra
			// life, disble broken heart.
			if (value)
			{
				if (_brokenHearts[_currentLives] != null)
				{
					_brokenHearts[_currentLives].SetActive(false);
				}
				_hearts[_currentLives].SetActive(true);
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

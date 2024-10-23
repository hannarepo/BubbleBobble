using UnityEngine;
using TMPro;

namespace BubbleBobble
{
		public class Health : MonoBehaviour
		{
		[SerializeField] private int _maxLives = 4;
		[SerializeField] private Transform _playerReturnPoint;
		[SerializeField] private TMP_Text _gameOverText;
		[SerializeField] private float _invincibilityTime = 1f;
		[SerializeField] private float _flashRate = 1 / 10f;
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

		private void Awake()
		{
			_transform = transform;
			_currentLives = _maxLives;
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_inputReader = GetComponent<InputReader>();
			_rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
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
			if (collision.gameObject.CompareTag("Enemy") && !IsInvincible)
			{
				_currentLives--;
				Invoke("Respawn", 1f);

				if (_currentLives > 0)
				{
					_invincibilityTimer = _invincibilityTime;
				}
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

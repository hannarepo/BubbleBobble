using UnityEngine;

namespace MemoBubble
{
	/// <summary>
	/// An undefeatable enemy that cannot be trapped and follows the player.
	/// Stops at given intervals for a given time and then starts moving again.
	/// Sound effect is played every time movement starts.
	/// </summary>
    public class Undefeatable : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;
		[SerializeField] private float _stopInterval = 2f;
		[SerializeField] private float _stopTime = 2f;
		[SerializeField] private Transform _startPosition;
		[SerializeField] private AudioClip _bossSFX;
		[SerializeField] private Audiomanager _audioManager;
		private Rigidbody2D _rb;
		private GameObject _player;
		private float _timer = 0f;
		private SpriteRenderer _spriteRenderer;

		private void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_player = GameObject.FindWithTag(Tags.Player);
			_timer = _stopInterval;
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void OnEnable()
		{
			transform.position = _startPosition.position;
		}

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_timer < _stopInterval)
			{
				transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
			}
			else if (_timer > _stopInterval && _timer < _stopTime)
			{
				return;
			}
			else
			{
				_timer = 0;
			}

			if (_timer == 0)
			{
				PlaySFXOnMove();
			}

			if (_player.transform.position.x < transform.position.x)
			{
				_spriteRenderer.flipX = false;
			}
			else
			{
				_spriteRenderer.flipX = true;
			}
		}

		private void PlaySFXOnMove()
		{
			_audioManager.PlaySFX(_bossSFX);
		}
    }
}

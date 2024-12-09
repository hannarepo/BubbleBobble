using UnityEngine;

namespace BubbleBobble
{
    public class Undefeatable : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;
		[SerializeField] private float _stopInterval = 2f;
		[SerializeField] private float _stopTime = 2f;
		[SerializeField] private Transform _startPosition;
		private Rigidbody2D _rb;
		private GameObject _player;
		private float _timer = 0f;

		private void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_player = GameObject.FindWithTag(Tags.Player);
			_timer = _stopInterval;
		}

		private void OnEnable()
		{
			transform.position = _startPosition.position;
		}

		private void FixedUpdate()
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
		}
    }
}

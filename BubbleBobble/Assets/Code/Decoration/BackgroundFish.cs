using UnityEngine;

namespace BubbleBobble
{
    public class BackgroundFish : MonoBehaviour
    {
        [SerializeField] private Transform _startPos;
		[SerializeField] private float _endXPos;
		[SerializeField] private float _speed;
		[SerializeField] Vector3 _direction = new Vector3(-1, -1, 0);

		private void Update()
		{
			if (transform.position.x >= _endXPos)
			{
				transform.position += _direction * _speed * Time.deltaTime;
			}
			else
			{
				transform.position = _startPos.position;
			}
		}
    }
}

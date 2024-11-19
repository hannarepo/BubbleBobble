using UnityEngine;

namespace BubbleBobble
{
	public class PlayerPosition : MonoBehaviour
	{
		public static Vector2 _playerPosition;

		private void Update()
		{
			_playerPosition = transform.position;
		}	
		
	}
}

using UnityEngine;

namespace BubbleBobble
{
	public class PowerUp : MonoBehaviour
	{
		[SerializeField] private PowerUpType _powerUpType;
		//[SerializeField] private bool _activatePowerUp = false;
		[SerializeField] private GameObject _player;

		public void ActivatePowerUp()
		{
				if (_powerUpType == PowerUpType.Speed)
			{
				PlayerMover mover = _player.GetComponent<PlayerMover>();

				if (mover != null)
				{
					mover.SpeedBoostIsActive = true;
				}
			}
		}
	}
}

using UnityEngine;

namespace BubbleBobble
{
	public class PowerUp : MonoBehaviour
	{
		[SerializeField] private PowerUpType _powerUpType;
		[SerializeField] private GameObject _player;

		public void ActivatePowerUp()
		{
			switch (_powerUpType)
			{
				case PowerUpType.Speed:
					PlayerMover mover = _player.GetComponent<PlayerMover>();
					if (mover != null)
					{
						mover.SpeedBoostIsActive = true;
					}
					break;
				case PowerUpType.BubbleSpeed:
					ShootBubble shoot = _player.GetComponent<ShootBubble>();
					if (shoot != null)
					{
						shoot.ForceBoostIsActive = true;
					}
					break;


			}
		}
	}
}

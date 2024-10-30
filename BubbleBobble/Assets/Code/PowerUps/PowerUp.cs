using UnityEngine;

namespace BubbleBobble
{
	public class PowerUp : MonoBehaviour
	{
		[SerializeField] private PowerUpType _powerUpType;
		[SerializeField] private GameObject _player;

		public void ActivatePowerUp()
		{
			PlayerMover mover = _player.GetComponent<PlayerMover>();
			ShootBubble shoot = _player.GetComponent<ShootBubble>();
			PlayerControl playerControl = _player.GetComponent<PlayerControl>();
			Health health = _player.GetComponent<Health>();

			switch (_powerUpType)
			{
				case PowerUpType.Speed:
					if (mover != null)
					{
						mover.SpeedBoostIsActive = true;
					}
					break;
				case PowerUpType.BubbleSpeed:
					
					if (shoot != null)
					{
						shoot.ForceBoostIsActive = true;
					}
					break;
				case PowerUpType.FireRate:
					if (playerControl != null)
					{
						playerControl.FireRateBoostIsActive = true;
					}
					break;
				case PowerUpType.BubbleSize:
					if (shoot != null)
					{
						shoot.SizeBoostIsActive = true;
					}
					break;
				case PowerUpType.ExtraLife:
					if (health != null && health.CurrentLives < health.MaxLives)
					{
						health.SetExtraLife(true);
					}
					break;
			}
		}
	}
}

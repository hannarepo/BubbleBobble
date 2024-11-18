using TMPro;
using UnityEngine;

namespace BubbleBobble
{
	public class SpeedBoost : PowerUp
	{
		private PlayerMover _mover;

		protected override void Start()
		{
			base.Start();
			_mover = _player.GetComponent<PlayerMover>();
		}

		public override void PowerUpTimer()
		{
			_timerImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
			_timerText.text = $"{(int)_powerUpTime - (int)_timer}";
		}

		public override void ActivatePowerUp()
		{
			if (_mover != null)
			{
				_mover.SpeedBoostIsActive = true;
				_isActive = true;
			}
			base.ActivatePowerUp();
		}

		public override void DeactivatePowerUp()
		{
			if (_mover != null)
			{
				_mover.SpeedBoostIsActive = false;
			}
			base.DeactivatePowerUp();
		}

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}

using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Give player a fire rate boost to let them shoot bubbles faster.
	/// </summary>
    public class FireRateBoost : PowerUp
    {
		private PlayerControl _playerControl;

        protected override void Start()
		{
			base.Start();
			_playerControl = _player.GetComponent<PlayerControl>();
		}

		public override void PowerUpTimer()
		{
			_timerImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
			_timerText.text = $"{(int)_powerUpTime - (int)_timer}";
		}

		public override void ActivatePowerUp()
		{
			if (_playerControl != null)
			{
				_playerControl.FireRateBoostIsActive = true;
				_isActive = true;
			}
			base.ActivatePowerUp();
		}

		public override void DeactivatePowerUp()
		{
			if (_playerControl != null)
			{
				_playerControl.FireRateBoostIsActive = false;
			}
			base.DeactivatePowerUp();
		}

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}

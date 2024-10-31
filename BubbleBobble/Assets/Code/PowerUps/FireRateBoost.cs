using UnityEngine;

namespace BubbleBobble
{
    public class FireRateBoost : PowerUp
    {
		private PlayerControl _playerControl;
        protected override void Start()
		{
			base.Start();
			_playerControl = _player.GetComponent<PlayerControl>();
		}

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_timer >= _powerUpTime)
			{
				_playerControl.FireRateBoostIsActive = false;
				SetActiveStatus(false);
			}
		}

		public override void ActivatePowerUp()
		{
			if (_playerControl != null)
			{
				_playerControl.FireRateBoostIsActive = true;
			}
			base.ActivatePowerUp();
		}
	}
}

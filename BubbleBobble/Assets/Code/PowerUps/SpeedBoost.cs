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

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_timer >= _powerUpTime)
			{
				_mover.SpeedBoostIsActive = false;
				SetActiveStatus(false);
			}
		}

		public override void ActivatePowerUp()
		{
			if (_mover != null)
			{
				_mover.SpeedBoostIsActive = true;
			}
			base.ActivatePowerUp();
		}
	}
}

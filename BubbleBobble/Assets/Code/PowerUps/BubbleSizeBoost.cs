using UnityEngine;

namespace BubbleBobble
{
	public class BubbleSizeBoost : PowerUp
	{
		private ShootBubble _shoot;

		protected override void Start()
		{
			base.Start();
			_shoot = _player.GetComponent<ShootBubble>();
		}

		private void Update()
		{
			_timer += Time.deltaTime;

			if (_timer >= _powerUpTime)
			{
				_shoot.SizeBoostIsActive = false;
				SetActiveStatus(false);
			}
		}

		public override void ActivatePowerUp()
		{
			if (_shoot != null)
			{
				_shoot.SizeBoostIsActive = true;
			}
			base.ActivatePowerUp();
		}
	}
}

using UnityEngine;

namespace BubbleBobble
{
    public class BubbleForceBoost : PowerUp
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
				_shoot.ForceBoostIsActive = false;
				SetActiveStatus(false);
			}
		}

		public override void ActivatePowerUp()
		{
			if (_shoot != null)
			{
				_shoot.ForceBoostIsActive = true;
			}
			base.ActivatePowerUp();
		}
	}
}

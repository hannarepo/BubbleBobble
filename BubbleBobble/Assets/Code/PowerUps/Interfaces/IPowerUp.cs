using UnityEngine;

namespace BubbleBobble
{
    public interface IPowerUp
    {
        void ActivatePowerUp();
		void DeactivatePowerUp();
		void PowerUpTimer();
		void SetPriceColor(Color color);
		void SetActiveStatus(bool isActive);
    }
}
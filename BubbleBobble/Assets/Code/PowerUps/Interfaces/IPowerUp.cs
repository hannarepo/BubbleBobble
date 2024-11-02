using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public interface IPowerUp
    {
        void ActivatePowerUp();
		void SetPriceColor(Color color);
		void SetActiveStatus(bool isActive);
    }
}

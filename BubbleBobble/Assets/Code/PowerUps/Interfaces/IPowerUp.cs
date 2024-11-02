using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public interface IPowerUp
    {
        void ActivatePowerUp(bool activate);
		void SetPriceColor(Color color);
		void SetActiveStatus(bool isActive);
    }
}

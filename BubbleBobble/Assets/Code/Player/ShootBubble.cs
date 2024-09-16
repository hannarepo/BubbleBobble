using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class ShootBubble : MonoBehaviour
    {
        private GameObject _bubble;

        private void Awake()
        {
            _bubble = Resources.Load("Prefabs/Bubble") as GameObject;
        }

        public void Shoot(bool shoot)
        {
            if (shoot)
            {
                Instantiate(_bubble, transform.position, Quaternion.identity);
            }
        }
    }
}
